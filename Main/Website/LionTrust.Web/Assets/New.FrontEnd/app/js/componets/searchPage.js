import Vue from "vue/dist/vue.common.prod";

export default () => {
  const host = "http://localhost:3004/results?";
  new Vue({
    el: "#result-list-app",
    data: {
      results: [],
      amountResults: 70,
      showPerPage: 5,
      showPageInPagination: 7,
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
        console.log("query", query);
        return query;
      },
      getPage() {
        return this.searchParams.page;
      },
      getPageAmount() {
        return Math.floor(this.amountResults / this.showPerPage);
      },
      getQueryText() {
        return this.searchParams.queryText;
      },
      showPages() {
        return this.getPage;
      },

      getPages() {
        const pages = [];
        const median = Math.floor(this.showPageInPagination / 2); // 3
        const step = this.getPage - median; // 5 - 4 = 1
        // console.log("median", median);
        // console.log("this.getPage", this.getPage);
        // console.log("startIndex", step);
        // console.log("this.getPage", this.getPage);
        // console.log("this.getPageAmount", this.getPageAmount);

        for (let i = 0; i < this.showPageInPagination; i++) {
          console.log("i", i + step);
          // if (this.getPage >= this.getPage + step) {
          //   console.log('here');
          //   pages.push(i + 1);
          // 7                14 - 3 = 11
          if (this.getPage >= this.getPageAmount - step) console.log("less");
          if (this.getPage <= median) {
            pages.push(i + 1);
          } else {
            pages.push(i + step);
          }
        }
        console.log("pages", pages);

        return pages;

        // if (sum > this.getPage + 1) {
        //   let x = sum - numPage;
        //   console.log('x',x);
        //   return x + 1;
        // }
      },
      showForwardPageBtn() {
        return !this.getPages.includes(1);
      },
      showNextPageBtn() {
        return !this.getPages.includes(this.getPageAmount);
      },
    },
    methods: {
      jumpPagesForward() {
        this.searchParams.page = this.getPage - this.showPageInPagination;
      },
      jumpPagesNext() {
        this.searchParams.page = this.getPage + this.showPageInPagination;
      },
      labelClick() {
        this.allResults = false;
      },
      submit() {
        this.searchParams.type = "";
        this.searchParams.page = 1;
        this.serchRequest();
      },
      serchRequest(params = this.generateSearchParams) {
        this.changeUrl(params);
        $.ajax(host + params)
          .done((res) => {
            this.results = res;
          })
          .fail((e) => {
            throw new Error("Data does not fetch " + e);
          });
      },
      changeUrl(searchParams) {
        console.log("changeUrl");
        window.history.pushState(
          { page: "search-page" },
          "search",
          `${window.location.href.split("?")[0]}?${searchParams}`
        );
      },
      prevPage() {
        if (this.getPage > 1) {
          this.changePage(this.getPage - 1);
        }
      },
      nextPage() {
        const nextPage = this.getPage + 1;
        if (nextPage <= this.getPageAmount) {
          this.changePage(nextPage);
        }
      },
      changePage(num) {
        console.log("num", num);
        this.searchParams.page = num;
        this.serchRequest();
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
          console.log("type", type);
          this.searchParams.type = type;
          this.serchRequest();
        },
        deep: true,
      },
    },
    mounted() {
      console.log("this.searchParams.queryText", this.getReqValue.queryText);
      this.serchRequest(this.getReqValue.queryText);
    },
  });
};
