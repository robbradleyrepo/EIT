import Vue from "vue/dist/vue.common.prod";
import { API } from "./listFilter/api/api";
import {
  baseDownloadChild,
  baseDownloadParent,
} from "./listFilter/mixins/baseDownload";
import { pagination } from "./listFilter/mixins/pagination";
export default () => {
  const rootDom = document.getElementById("lister-app");
  let host = rootDom.dataset?.host;
  const literatureId = rootDom.dataset?.literatureid;
  const fundFacetId = rootDom.dataset?.fundfacetid;
  const folderId = rootDom.dataset?.folderid;
  const parentId = rootDom.dataset?.parentid;
  const contentType = rootDom.dataset?.contenttype;
  const ref = rootDom.dataset?.ref;
  const funds = rootDom.dataset?.funds;
  const location = "https://cm-liontrust-it.sagittarius.agency/";
  let root = "";
  if (
    window.location.hostname === "localhost" ||
    window.location.hostname === "127.0.0.1"
  ) {
    host = location + host;
    root = location;
  } else {
    host = "/" + host;
  }

  const months = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December",
  ];  

  const optionField = Vue.component("option-field", {
    name: "option-field",
    data: function () {
      return {
        checked: false,
      };
    },
    methods: {
      clearChecked() {
        this.checked = false;
      },
    },
    created: function () {
      this.$parent.$on("clearOptionField", this.clearChecked);
    },
  });

  const selectField = Vue.component("select-field", {
    name: "select-field",
    components: { optionField },
    data: function () {
      return {
        open: false,
        active: 0,
      };
    },
    methods: {
      toggleOption() {
        this.$parent.$children.forEach(child =>  {
            child.open = false
          })
        this.open = !this.open;
      },
      hideOption() {
        this.open = false;
      },
      clearOption() {
        this.active = 0;
        this.$emit("clearOptionField");
      },
      setChoosen(val) {
        if (val) this.active++;
        else this.active--;
      },
    },
    mounted() {
      document.querySelector("body").addEventListener("click", () => {
        this.open = false;
      });
    },
    created() {
      this.$parent.$on("clearOption", this.clearOption);
    },
  });

  const articleItem = Vue.component("article-item", {
    name: "article-item",
    data: function () {
      return {};
    },
    methods: {
      showLiteratureOverlay(fundId) {
        $.ajax({
          url: `${root}/api/sitecore/FundLiterature/GetOverlayHtml?fundId=${fundId}&literatureId=${literatureId}`,
        }).done(function (data) {
          $(".lit-overlay__wrapper").html(data).addClass("active");
        });
      },
    },
  });  

  const fundItem = Vue.component("fund-item", {
    name: "fund-item",
    data: function () {
      return {};
    },
  });

  const documentItem = Vue.component("document-item", {
    name: "document-item",
    props: ["id", "title"],
    mixins: [baseDownloadChild],
    data: function () {
      return {
        selected: false,
      };
    },
    methods: {
      selectDocument() {
        this.$parent.setDocumentId(this.id);
      },
      downloadDocument() {
        API.downloadDocument(
          `${host}/DownloadDocuments`,
          {
            downloadFileIds: this.id,
          },
          this.title,
          ".pdf"
        );
      },
    },
  });

  new Vue({
    el: "#lister-app",
    mixins: [pagination, baseDownloadParent],
    components: { selectField, articleItem, documentItem },
    data: {
      facets: {},
      params: {},
      page: 1,
      searchText: "",
      searchData: [],
      loading: true,
      init: true,
      sortModal: false,
      sortOrder: "ASC",
      amountResults: 0,
      showPerPage: 21,
      showPageInPagination: 7,
      mobileFilter: false,
      months: [],
      years: [],
      month: "All",
      year: "All",
      grid: false,
      activeButton: false,
      isCheckAll: false,

    },
    computed: {
      getFacets() {
        return this.facets;
      },
      getFacetsLength() {
        return this.facets.length;
      }, 
      checkedLength () {
        return this.facets.filter(item => item.checked).length
      },
  
      allItemsChecked () {
        return this.checkedLength === this.facets.length
      }
    },
    methods: {
      // adding selected values to query params
      toggleSelect(item, facet) {
        if (!this.params[facet.name]) Vue.set(this.params, facet.name, []);
        const existElem = this.params[facet.name].findIndex((el) => {
          return el === item.identifier;
        });

        if (existElem !== -1) this.params[facet.name].splice(existElem, 1);
        else this.params[facet.name].push(item.identifier);
      },
      hideSelects() {
        console.log('hide');
      },
      checkAll(){
        let futureCheckedValue = true

        if (this.allItemsChecked) {
          futureCheckedValue = false
        }

        this.facet.items.forEach(item => item.checked = futureCheckedValue)
      },
      getQueryString() {
        let str = "";
        str = str + "page=" + this.page;
        if (this.searchText) str = str + "&searchTerm=" + this.searchText;
        if (ref) str = str + "&ref=" + ref;
		    if(funds) str = str + "&ids=" + funds;
        for (let prop in this.params) {
          const mutatedProp = prop.replace(/ /g, "");
          const lowerCaseProp =
            mutatedProp.charAt(0).toLowerCase() + mutatedProp.substr(1);
          if (this.params[prop].length)
            str += "&" + `${lowerCaseProp}=${this.params[prop].join("|")}`;
        }
        if (contentType) str += `&contentType=${contentType}`;

        return str;
      },

      pushStateLink() {
        window.history.pushState(
          { page: "article-lister" },
          "search",
          `${window.location.href.split("?")[0]}?${this.getQueryString()}`
        );
      },

      applyFilters() {
        // this.pushStateLink();
        this.mobileFilter = false;
        this.getSearchRequest();
        this.activeButton = false;
      },

      clearFilters() {
        this.params = {};
        this.page = 1;
        this.open = false;
        this.searchText = "";
        this.month = this.year = "All";
        this.$emit("clearOption");
        this.mobileFilter = false;
        this.applyFilters();
      },

      setMonth(month) {
        if (month === "All") this.params.month = [];
        else this.params.month = [month];
      },

      setYear(year) {
        if (year === "All") this.params.year = [];
        else this.params.year = [year];
      },

      showSort() {
        this.sortModal = true;
      },

      changePage(num) {
        if (this.getPage !== num) {
          this.scrollToTop();
          this.page = num;
          this.applyFilters();
        }
      },

      toggleMobileFilter() {
        this.mobileFilter = !this.mobileFilter;
      },

      submitSearchForm(e) {
        if (e.target.searchText.value) this.applyFilters();
      },

      clearDocumentIds() {
        this.selectedDocumentIds = [];
      },

      downloadDocumentMultiple() {
        API.downloadDocument(
          `${host}/DownloadDocuments`,
          { downloadFileIds: this.selectedDocumentIds },
          "Documents",
          ".zip"
        );
      },

      getFacetsRequest() {
        const facetUrl = fundFacetId
          ? `${host}/Facets?facetConfig={${fundFacetId}}`
          : `${host}/Facets`;
        $.get(facetUrl)
          .done((response) => {
            const { facets, dates } = response;
            this.facets = facets;

            if (dates && dates.months)
              for (let i in dates.months) {
                this.months.push(months[i]);
              }
            if (dates && dates.years) this.years = dates.years;
          })
          .fail((e) => {
            console.error(e);
            this.loading = false;
          });
      },

      getSearchRequest() {
        this.loading = true;
        let hostUrl;
        if (folderId)
          hostUrl = `${host}/GetDocuments?documentFolderId={${folderId}}&`;
        else if (parentId) hostUrl = `${host}/Search?parentId={${parentId}}&`;
        else hostUrl = host + "/Search?";

        $.get(hostUrl + this.getQueryString())
          .done((response) => {
            const { searchResults, totalResults } = response;
            this.searchData = searchResults;
            this.amountResults = totalResults;
            this.loading = false;
            this.activeButton = false;
          })
          .fail((e) => {
            console.error(e);
            this.loading = false;
            this.searchData = [];
            this.amountResults = 0;
            this.activeButton = false;
          });
      },
    },
    watch: {
      sortOrder: function () {
        this.params.sortOrder = [this.sortOrder];
        this.applyFilters();
      },
      params: {
        deep: true,
        handler: function () {
          this.activeButton = true;
        },
      },
    },
    mounted() {
      this.getFacetsRequest();
      this.getSearchRequest();
      // this.facets = facets;
      // const { SearchResults, TotalResults } = responce;
      // this.searchData = results;
      // this.amountResults = TotalResults;

      document.querySelector("body").addEventListener("click", () => {
        this.sortModal = false;
      });
    },
  });
};
