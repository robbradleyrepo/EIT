import Vue from "vue/dist/vue.common.prod";
import { pagination } from "./listFilter/mixins/pagination";
export default () => {
  const rootDom = document.getElementById("lister-app");
  let host = rootDom.dataset.host;
  const literatureId = rootDom.dataset.literatureid;
  const fundFacetId = rootDom.dataset.fundfacetid;
  const folderId = rootDom.dataset.folderid;
  const location = "https://cm-liontrust-it.sagittarius.agency/";
  let root = "";
  if (
    window.location.hostname == "localhost" ||
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
  new Vue({
    el: "#lister-app",
    mixins: [pagination],
    data: {
      facets: {},
      params: {},
      page: 1,
      searchText: "",
      searchData: [],
      loading: true,
      sortModal: false,
      sortOrder: "ASC",
      amountResults: 0,
      showPerPage: 21,
      showPageInPagination: 7,
      mobileFilter: false,
      months: [],
      years: [],
      grid: false,
      selectAllDocuments: false,
      selectedDocumentIds: [],
    },
    computed: {
      getFacets() {
        const res = {};
        for (let i in this.facets) {
          res[i] = this.facets[i];
        }
        return res;
      },
    },
    methods: {
      // adding selected values to query params
      toggleSelect(item, facet) {
        if (!this.params[facet.name]) this.params[facet.name] = [];
        const existElem = this.params[facet.name].findIndex((el) => {
          return el === item.identifier;
        });

        if (existElem !== -1) this.params[facet.name].splice(existElem, 1);
        else this.params[facet.name].push(item.identifier);
      },

      getQuerySring() {
        let str = "";
        str = str + "page=" + this.page;
        if (this.searchText) str = str + "&searchTerm=" + this.searchText;
        for (let prop in this.params) {
          const mutatedProp = prop.replace(/ /g, "");
          const lowerCaseProp =
            mutatedProp.charAt(0).toLowerCase() + mutatedProp.substr(1);
          if (this.params[prop].length)
            str += "&" + `${lowerCaseProp}=${this.params[prop].join("|")}`;
        }
        return str;
      },

      pushStateLink() {
        window.history.pushState(
          { page: "article-lister" },
          "search",
          `${window.location.href.split("?")[0]}?${this.getQuerySring()}`
        );
      },

      applyFilters() {
        this.pushStateLink();
        this.mobileFilter = false;
        this.getSearchRequest();
      },

      clearFilters() {
        this.params = {};
        this.page = 1;
        this.open = false;
        this.searchText = "";
        this.$emit("clearOption");
        this.mobileFilter = false;
        this.applyFilters();
      },

      setMonth(e) {
        this.params.month = [e.target.value];
      },

      setYear(e) {
        this.params.year = [e.target.value];
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

      setDocumentId(id) {
        const index = this.selectedDocumentIds.findIndex((el) => el === id);
        if (index !== -1) this.selectedDocumentIds.splice(index, 1);
        else this.selectedDocumentIds.push(id);
        console.log('this.selectedDocumentIds',this.selectedDocumentIds);
      },
      
      dowloadDocument(title, id) {
        $.post(
          `https://cm-liontrust-it.sagittarius.agency/DocumentsApi/DownloadDocuments?downloadFileIds={${id}}`
        ).done((data) => {
          var blob = new Blob([data]);
          var link = document.createElement("a");
          link.href = window.URL.createObjectURL(blob);
          link.download = title + '.zip';
          link.click();
        });
      },

      downloadDocumentMultiple() {
        const docsIds = this.selectedDocumentIds.join();
        this.dowloadDocReq('Document archive', docsIds)
      },

      getFacetsRequest() {
        console.log("fundFacetId", fundFacetId);
        const facetUrl = fundFacetId
          ? `${host}/Facets?fundListingFacetConfig=${fundFacetId}`
          : `${host}/Facets`;
        $.get(facetUrl)
          .done((responce) => {
            const { facets, dates } = responce;
            console.log("facets", facets);
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
        $.get(host + "/Search?" + this.getQuerySring())
          .done((responce) => {
            const { searchResults, totalResults } = responce;
            console.log("searchResults", searchResults);
            this.searchData = searchResults;
            this.amountResults = totalResults;
            this.loading = false;
          })
          .fail((e) => {
            console.error(e);
            this.loading = false;
          });
      },
    },
    watch: {
      sortOrder: function () {
        this.params.sortOrder = [this.sortOrder];
        this.applyFilters();
      },
    },
    mounted() {
      this.getFacetsRequest();
      this.getSearchRequest();   
      // this.facets = facets;
      // const { SearchResults, TotalResults } = responce;
      // this.searchData = SearchResults;
      // this.amountResults = TotalResults;  

      document.querySelector("body").addEventListener("click", () => {
        this.sortModal = false;
      });
    },
  });

  Vue.component("select-field", {
    data: function () {
      return {
        open: false,
      };
    },
    methods: {
      toggleOption() {
        this.open = !this.open;
      },
      clearOption() {
        this.$emit("clearOptionField");
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

  Vue.component("option-field", {
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

  Vue.component("article-item", {
    data: function () {
      return {};
    },
    methods: {
      showLiteratureOverlay(fundId) {
        $.ajax({
          url: `${root}api/sitecore/FundLiterature/GetOverlayHtml?fundId=${fundId}&literatureId=${literatureId}`,
        }).done(function (data) {
          console.log('data',data);
          $(".lit-overlay__wrapper").html(data).addClass("active");
        });
      },
    },
  });

  Vue.component("fund-item", {
    data: function () {
      return {};
    },
  });

  Vue.component("document-item", {
    data: function () {
      return {
        selected: false,
      };
    },
    methods: {
      selectDocument(id) {
        this.$parent.setDocumentId(id);
      },
    },
  });
};
