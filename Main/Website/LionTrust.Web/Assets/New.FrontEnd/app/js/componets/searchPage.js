import Vue from "vue/dist/vue.common.prod";
import { pagination } from "./listFilter/mixins/pagination";

const location = "https://cm-liontrust-it.sagittarius.agency/";
const rootDom = document.getElementById("result-list-app");
let root = "";
let host = rootDom.dataset.host;
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
      amountResults: 10,
      showPerPage: 10,
      showPageInPagination: 7,
      loading: true,
      searchParams: {
        query: "",
        type: "",
        page: 1,
        take: 10
      },
      allResults: true,
      labels: [
        { title: "Funds", categoryName: "Funds", checked: false },
        {
          title: "Funds Managers",
          categoryName: "Funds Managers",
          checked: false,
        },
        { title: "Articles", categoryName: "Article", checked: false },
        { title: "Documents", categoryName: "Document", checked: false },
        { title: "Pages", categoryName: "Page", checked: false },
      ],
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
        console.log('query',query);
        return query;
      },

      getQueryText() {
        return this.searchParams.query;
      },
      getFilterTopPosition() {
        return document.getElementsByTagName('body')[0].offsetTop;
      }
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
        const url = `${host}/search?${params}`
        console.log('url',url);
        this.loading = true;
        $.ajax(url)
          .done((request) => {
            const { searchResults, totalResults } = request;
            this.results = searchResults;
            this.amountResults = totalResults;
            console.log("request", request);
            this.loading = false;
          })
          .fail((e) => {
            this.loading = false;
            throw new Error("Data does not fetch ", e);
          });

      },
      getFacets() {        
        $.get(`${host}/facets`)
          .done((responce) =>{
            console.log('res', responce);
            this.labels = responce;
          }).fail((e) => {
            this.loading = false;
            throw new Error("Data does not fetch ", e);
          })
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
          let type = "";
          filters.forEach((item, i) => {
            if (filters.length - 1 !== i) type += `${item.categoryName},`;
            else type += `${item.categoryName}`;
          });
          this.searchParams.type = type;
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
      this.serchRequest();
      this.getFacets();

    },
  });
};
