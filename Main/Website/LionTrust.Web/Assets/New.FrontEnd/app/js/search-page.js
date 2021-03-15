import Vue from "vue/dist/vue.common.prod";

export default () => {
  const host = "http://localhost:3004/results?";
  const amountResults = 123;
  const showPerPage = 10;
  new Vue({
    el: "#result-list-app",
    data: {
      results: [],
      searchParams: {
        queryText: "",
        type: "",
        page: 1,
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
        for (const [key, value] of Object.entries(this.searchParams)) {
          if (value) query += `${key}=${value}&`;
        }
        console.log('query',query);
        return query;
      },
    },
    methods: {
      labelClick() {
        this.allResults = false;
      },
      submit() {
        this.searchParams.type = '',
        this.searchParams.page = 1
        this.serchRequest()
      },
      serchRequest(params = this.generateSearchParams) {
        this.changeUrl(params)
        $.ajax(host + params)
          .done((res) => {
            this.results = res;
          })
          .fail((e) => {
            throw new Error("Data does not fetch " + e);
          });
      },
      changeUrl(searchParams) {
        console.log('changeUrl');
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
          console.log('type',type);
          this.searchParams.type = type
          this.serchRequest();
        },
        deep: true,
      },
    },
    mounted() {
      this.serchRequest(this.searchParams.queryText);
    },
  });
};
