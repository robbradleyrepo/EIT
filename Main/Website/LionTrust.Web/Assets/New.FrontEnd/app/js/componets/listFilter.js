import Vue from "vue/dist/vue.common.prod";
import download from "downloadjs";
const eventBus = new Vue();
import { pagination } from "./listFilter/mixins/pagination";
export default () => {
  const rootDom = document.getElementById("lister-app");
  let host = rootDom.dataset.host;
  const literatureId = rootDom.dataset.literatureid;
  const fundFacetId = rootDom.dataset.fundfacetid;
  const folderId = rootDom.dataset.folderid;
  const parentId = rootDom.dataset.parentid;
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

  const results = [
    {
      title: "Liontrust GF Absolute Return Bond Fund Sales Aid",
      documentLink:
        "/sitecore/media-library/liontrust/files/fund-literature/sales-aids/offshore/liontrust-gf-absolute-return-bond-fund-sales-aid-2020-q2",
      documentLinkText:
        "Liontrust GF Absolute Return Bond Fund Sales Aid 2020 Q2",
      documentPageLink:
        "/sitecore/content/modules/documents/adviser-support/liontrust-gf-absolute-return-bond-fund-sales-aid",
      documentVideoLink: null,
      documentId: "567e5295-f425-4825-84a0-ed9a0e5282a9",
    },
    {
      title: "Liontrust GF Absolute Return Bond Fund Sales Aid (International)",
      documentLink:
        "/-/media/liontrust/files/fund-literature/sales-aids/offshore/liontrust-gf-absolute-return-bond-fund-sales-aid-2020-q2-international.ashx",
      documentLinkText: "",
      documentPageLink:
        "/sitecore/content/modules/documents/adviser-support/liontrust-gf-absolute-return-bond-fund-sales-aid-international",
      documentVideoLink: null,
      documentId: "861af72c-8a93-4f1f-8079-5848c32ff349",
    },
    {
      title: "Liontrust GF High Yield Bond Fund Sales Aid",
      documentLink:
        "/-/media/liontrust/files/fund-literature/sales-aids/offshore/liontrust-gf-high-yield-fund-sales-aid-2020-q2.ashx",
      documentLinkText: "",
      documentPageLink:
        "/sitecore/content/modules/documents/adviser-support/liontrust-gf-high-yield-bond-fund-sales-aid",
      documentVideoLink: null,
      documentId: "7d0287be-36a8-4ad0-81fb-f0a9fdec2435",
    },
    {
      title: "Liontrust GF High Yield Bond Fund Sales Aid (International)",
      documentLink:
        "/sitecore/media-library/liontrust/files/fund-literature/sales-aids/offshore/liontrust-gf-high-yield-fund-sales-aid-2020-q2-international",
      documentLinkText:
        "Liontrust GF High Yield Fund Sales Aid 2020 Q2 International",
      documentPageLink:
        "/sitecore/content/modules/documents/adviser-support/liontrust-gf-high-yield-bond-fund-sales-aid-international",
      documentVideoLink: null,
      documentId: "64b3feea-8835-4f53-a982-8d20356f12aa",
    },
    {
      title: "Liontrust GF Strategic Bond Fund Sales Aid",
      documentLink:
        "/-/media/liontrust/files/fund-literature/sales-aids/offshore/liontrust-gf-strategic-bond-fund-sales-aid-2020-q2.ashx",
      documentLinkText: "",
      documentPageLink:
        "/sitecore/content/modules/documents/adviser-support/liontrust-gf-strategic-bond-fund-sales-aid",
      documentVideoLink: null,
      documentId: "ad2334a7-a0f0-4ad9-bd50-bcdae80bde2f",
    },
    {
      title: "Liontrust GF Sustainable Future Global Growth Fund Sales Aid",
      documentLink:
        "/-/media/liontrust/files/fund-literature/sales-aids/offshore/liontrust-gf-sf-global-growth-fund-sales-aid.ashx",
      documentLinkText: "",
      documentPageLink:
        "/sitecore/content/modules/documents/adviser-support/liontrust-gf-sustainable-future-global-growth-fund-sales-aid",
      documentVideoLink: null,
      documentId: "03b5904f-c94c-4a9d-8ff2-b671f5533421",
    },
    {
      title: "Liontrust Strategic Bond Fund Sales Aid",
      documentLink:
        "/-/media/liontrust/files/adviser-support/lt-strategic-bond-fund-sales-aid-build24-online-single-pages.ashx",
      documentLinkText: "",
      documentPageLink:
        "/sitecore/content/modules/documents/adviser-support/liontrust-strategic-bond-fund-sales-aid",
      documentVideoLink: null,
      documentId: "045b2230-0a55-43d3-9f2a-8bdca88cc52c",
    },
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

      getQueryString() {
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
          `${window.location.href.split("?")[0]}?${this.getQueryString()}`
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
      },

      clearDocumentIds() {
        this.selectedDocumentIds = [];
      },

      downloadDocumentMultiple() {
        document.body.style.cursor='wait';
        $.post({
          type: "POST",
          xhrFields: { responseType: "arraybuffer" },
          url: `${host}/DownloadDocuments`,
          data: {downloadFileIds: this.selectedDocumentIds},
        }).done((data) => {
          const url = window.URL.createObjectURL(new Blob([data]));
          const link = document.createElement("a");
          link.href = url;
          link.setAttribute("download", "Documents.zip");
          document.body.appendChild(link);
          link.click();
          document.body.style.cursor='default';
        })
        .fail((e) => {
          console.error(e);
          document.body.style.cursor='default';
        });
      },

      getFacetsRequest() {
        const facetUrl = fundFacetId
          ? `${host}/Facets?articleListingFacetConfig={${fundFacetId}}`
          : `${host}/Facets`;
        $.get(facetUrl)
          .done((response) => {
            const { facets, dates } = response;
            console.log("facets res", response);
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
      selectAllDocuments: function (value) {
        this.selectedDocumentIds = [];
        eventBus.$emit("toggleSelected", value);
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
    props: ["id", "title"],
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
        document.body.style.cursor='wait';
        $.post({
          xhrFields: { responseType: "arraybuffer" },
          url: `${host}/DownloadDocuments`,
          data: {
            downloadFileIds: this.id,
          },
        }).done((data) => {
          const url = window.URL.createObjectURL(
            new Blob([data], { type: "application/pdf;charset=base-64" })
          );
          const link = document.createElement("a");
          link.href = url;
          link.setAttribute("download", this.title + ".pdf");
          document.body.appendChild(link);
          link.click();
          document.body.style.cursor='default';
        }).fail((e) => {
          console.error(e);
          document.body.style.cursor='default';
        });
      },
    },
    created() {
      eventBus.$on("toggleSelected", (selected) => {
        this.selected = selected;
        if (selected) this.selectDocument(this.id);
      });
    },
  });
};
