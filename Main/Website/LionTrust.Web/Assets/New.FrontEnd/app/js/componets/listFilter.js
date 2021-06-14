import Vue from "vue/dist/vue.common.prod";
import { pagination } from "./listFilter/mixins/pagination";
export default () => {
  const host = "http://localhost:3004/article-lister?";
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
      sortValue: "ASC",
      amountResults: 0,
      showPerPage: 3,
      showPageInPagination: 7,
      mobileFilter: false,
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
          return el === item.Identifier;
        });

        if (existElem !== -1) this.params[facet.name].splice(existElem, 1);
        else this.params[facet.name].push(item.Identifier);
      },

      getQuerySring() {
        let str = "";
        str = str + "page=" + this.page;
        if (this.searchText) str = str + "searchTerm=" + this.searchText;
        for (let prop in this.params) {
          const mutatedProp = prop.replace(/ /g, "");
          const lowerCaseProp =
            mutatedProp.charAt(0).toLowerCase() + mutatedProp.substr(1);
          if (this.params[prop].length)
            str += "&" + `${lowerCaseProp}=${this.params[prop].join("|")}`;
        }
        console.log("str", str);
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
        this.loading = true;
        this.mobileFilter = false;
        $.get(
          "https://cm-liontrust-it.sagittarius.agency/ArticleSearchApi/Search?" +
            this.getQuerySring()
        ).done((responce) => {
          const { SearchResults, TotalResults } = responce;
          this.searchData = SearchResults;
          this.amountResults = TotalResults;
          console.log("SearchResults", SearchResults);
          console.log("this.amountResults", this.amountResults);
          this.loading = false;
        });
      },

      clearFilters() {
        this.params = {};
        this.page = 1;
        this.open = false;
        this.searchText = "";
        this.$emit("clearOption");
        this.mobileFilter = false;
        //  this.pushStateLink();
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
          this.page = num;
          this.params.page = [num];
          this.applyFilters();
        }
      },
      toggleMobileFilter() {
        this.mobileFilter = !this.mobileFilter;
      },
    },
    watch: {
      sortValue: function () {
        this.params.sortValue = [this.sortValue];
        this.applyFilters();
        console.log("this.params", this.params);
      },
    },
    mounted() {
      $.get(
        "https://cm-liontrust-it.sagittarius.agency/ArticleSearchApi/Facets"
      ).done((responce) => {
        const facets = [];
        for (let i in responce.Facets) {
          const name = i.replace(/([a-z])([A-Z])/g, "$1 $2");
          facets.push({
            name,
            data: responce.Facets[i],
          });
        }
        this.facets = facets;
      });

      $.get(
        "https://cm-liontrust-it.sagittarius.agency/ArticleSearchApi/Search?page=1"
      ).done((responce) => {
        const { SearchResults, TotalResults } = responce;
        this.searchData = SearchResults;
        console.log("this.searchData", responce);
        this.amountResults = TotalResults;
        console.log("this.amountResults", this.amountResults);
        this.loading = false;
      });

      document.querySelector("body").addEventListener("click", () => {
        this.sortModal = false;
      });
    },
  });
};

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
});
