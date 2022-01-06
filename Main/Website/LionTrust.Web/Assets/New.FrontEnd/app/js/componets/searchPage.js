import Vue from "vue/dist/vue.common.prod";
import { pagination } from "./listFilter/mixins/pagination";
const location = "https://cm-liontrust-it.sagittarius.agency/";
const rootDom = document.getElementById("result-list-app");
let root = "";
let host = rootDom.dataset.host;
const pageSize = rootDom.dataset.pagesize;
if (
  window.location.hostname === "localhost" ||
  window.location.hostname === "127.0.0.1"
) {
  host = location + host;
  root = location;
} else {
  host = "/" + host;
}
export default () => {
  new Vue({
    el: "#result-list-app",
    mixins: [pagination],
    data: {
      results: [],
      init: false,
      amountResults: 0,
      showPerPage: pageSize,
      showPageInPagination: 7,
      loading: true,
      searchParams: {
        query: "",
        filters: "",
        page: 1,
        take: pageSize,
      },
      allResults: true,
      labels: [],
    },
    computed: {
      getSearchResult() {
        return this.results;
      },
      getPage() {
        return this.searchParams.page;
      },
      getReqValue() {
        let match,
          pl = /\+/g, // Regex for replacing addition symbol with a space
          search = /([^&=]+)=?([^&]*)/g,
          decode = function (s) {
            return decodeURIComponent(s.replace(pl, " "));
          },
          query = window.location.search.substring(1);
        const urlParams = {};
        while ((match = search.exec(query)))
          urlParams[decode(match[1])] = decode(match[2]);
        return urlParams;
      },
      generateSearchParams() {
        let query = "";
        const entries = Object.entries(this.searchParams);
        entries.forEach(([key, value], index) => {
          if (value) query += `${key}=${value}&`;
        });
        return query;
      },
      getQueryText() {
        return this.searchParams.query;
      },
      getFilterTopPosition() {
        return document.getElementsByTagName("body")[0].offsetTop;
      },
    },
    methods: {
      labelClick() {
        this.allResults = false;
      },
      submit() {
        this.searchParams.page = 1;
        this.serchRequest();
      },
      serchRequest(params = this.generateSearchParams) {
        this.changeUrl(params);
        const url = `${host}/search?${params}`;
        this.loading = true;
        $.ajax(url)
          .done((request) => {
            const { searchResults, totalResults } = request;
            console.log('searchResults', searchResults);
            this.results = searchResults;
            this.amountResults = totalResults;
            this.loading = false;
          })
          .fail((e) => {
            this.loading = false;
            this.results = [];
            this.amountResults = 0;
            console.error("Data is not being retrieved", e)
          });
      },
      getFacets() {
        $.get(`${host}/facets`)
          .done(({ facets }) => {
            this.labels = facets.map((item) => {
              return { ...item, checked: false };
            });
          })
          .fail((e) => {
            this.loading = false;
            throw new Error("Facets does not fetch ", e);
          });
      },
      changeUrl(searchParams) {
        window.history.pushState(
          { page: "search-page" },
          "search",
          `${window.location.href.split("?")[0]}?${searchParams}`
        );
      },
    },
    watch: {
      allResults() {
        this.labels.forEach((label) => (label.checked = false));
      },
      labels: {
        handler(val) {
          const filters = val.filter((item) => item.checked);
          const type = [];
          filters.forEach((item) => {
            item.items.forEach((element) => {
              type.push(element.identifier);
            });
          });
          this.searchParams.filters = type.join("|");
          this.serchRequest();
        },
        deep: true,
      },
      results() {
        $("#mob-result").html(
          `${this.amountResults} results for “${this.getQueryText}”`
        );
      },
    },
    mounted() {
      this.searchParams.query = this.getReqValue.query;
      this.init = true;
      this.getFacets();
      this.serchRequest();
    },
  });
};
