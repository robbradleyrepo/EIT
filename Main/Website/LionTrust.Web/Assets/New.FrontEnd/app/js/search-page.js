import Vue from "vue/dist/vue.common.prod";

console.log("Vue", Vue);
export default () => {
  const host = "http://localhost:3004/results";
  const showPerPage = 2;
  new Vue({
    el: "#result-list-app",
    data: {
      inputText: "",
      results: [],
      allResults: true,
      labels: [
        { title: "Funds", checked: false },
        { title: "Funds Managers", checked: false },
        { title: "Articles", checked: false },
        { title: "Documents", checked: false },
        { title: "Pages", checked: false },
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
    },
    methods: {
      labelClick() {
        this.allResults = false;
      },
      submit() {
        console.log(this.inputText);
      },
      serchRequest() {
        $.ajax(host)
          .done((res) => {
            this.results = res;
          })
          .fail((e) => {
            throw new Error("Data does not fetch " + e);
          });
      },
    },
    watch: {
      allResults() {
        this.labels.forEach((label) => (label.checked = false));
        window.history.pushState(
          { page: "another" },
          "value",
          "http://localhost:3000/search-page.html"
        );
      },
    },
    mounted() {
      this.serchRequest();
      console.log("works");
      console.log("this.getReqValue", this.getReqValue);

      this.inputText = this.getReqValue.queryText;
    },
  });
};
