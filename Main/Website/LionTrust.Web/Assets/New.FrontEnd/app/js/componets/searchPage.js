import Vue from "vue/dist/vue.common.prod";
const request = {
  amountResults: "150",
  results: [
    {
      link: "#link",
      title: "Factsheet GF Absolute Return Bond Fund",
      bodyText:
        "Debt, debt, everywhere, nor any income to take. The current situation is not as bleak as this rewriting of Samuel Taylor Coleridge’s famous lines suggests, but with the UK and…",
      date: "21st February 2021",
      type: "Fund",
      footer: {
        factsheet: {
          name: "Factsheet",
          link: "#",
        },
        team: {
          name: "The Global Fixed Income team",
          link: "#",
        },
      },
    },
    {
      link: "#link",
      title: "Article GF Absolute Return Bond Fund",
      bodyText:
        "Debt, debt, everywhere, nor any income to take. The current situation is not as bleak as this rewriting of Samuel Taylor Coleridge’s famous lines suggests, but with the UK and…",
      date: "21st February 2021",
      type: "Article",
      footer: {
        author: {
          name: "David Roberts",
          imageSrc:
            "images/components/search-page/David-Roberts-Liontrust@2x.png",
        },
        team: {
          name: "The Global Fixed Income team",
          link: "#",
        },
      },
    },
    {
      link: "#link",
      title: "Page GF Absolute Return Bond Fund",
      bodyText:
        "Debt, debt, everywhere, nor any income to take. The current situation is not as bleak as this rewriting of Samuel Taylor Coleridge’s famous lines suggests, but with the UK and…",
      type: "Page",
      date: "23st December 2020",
    },
    {
      link: "#link",
      title: "Document GF Absolute Return Bond Fund",
      bodyText:
        "Debt, debt, everywhere, nor any income to take. The current situation is not as bleak as this rewriting of Samuel Taylor Coleridge’s famous lines suggests, but with the UK and…",
      type: "Document",
      date: "18st June 2020",
    },
    {
      link: "#link",
      title: "Document GF Absolute Return Bond Fund",
      bodyText:
        "Debt, debt, everywhere, nor any income to take. The current situation is not as bleak as this rewriting of Samuel Taylor Coleridge’s famous lines suggests, but with the UK and…",
      type: "Fund Manager",
      date: "1st June 2019",
    },
    {
      link: "#link",
      title: "Article GF Absolute Return Bond Fund",
      bodyText:
        "Debt, debt, everywhere, nor any income to take. The current situation is not as bleak as this rewriting of Samuel Taylor Coleridge’s famous lines suggests, but with the UK and…",
      date: "21st February 2021",
      type: "Article",
      footer: {
        author: {
          name: "David Roberts",
          imageSrc:
            "images/components/search-page/David-Roberts-Liontrust@2x.png",
        },
        team: {
          name: "The Global Fixed Income team",
          link: "#",
        },
      },
    },
    {
      link: "#link",
      title: "Page GF Absolute Return Bond Fund",
      bodyText:
        "Debt, debt, everywhere, nor any income to take. The current situation is not as bleak as this rewriting of Samuel Taylor Coleridge’s famous lines suggests, but with the UK and…",
      type: "Page",
      date: "23st December 2020",
    },
    {
      link: "#link",
      title: "Document GF Absolute Return Bond Fund",
      bodyText:
        "Debt, debt, everywhere, nor any income to take. The current situation is not as bleak as this rewriting of Samuel Taylor Coleridge’s famous lines suggests, but with the UK and…",
      type: "Document",
      date: "18st June 2020",
    },
    {
      link: "#link",
      title: "Document GF Absolute Return Bond Fund",
      bodyText:
        "Debt, debt, everywhere, nor any income to take. The current situation is not as bleak as this rewriting of Samuel Taylor Coleridge’s famous lines suggests, but with the UK and…",
      type: "Fund Manager",
      date: "1st June 2019",
    },
  ],
};

export default () => {
  const host = "http://localhost:3004/results?";
  new Vue({
    el: "#result-list-app",
    data: {
      results: [],
      amountResults: 0,
      showPerPage: 9,
      showPageInPagination: 7,
      loading: true,
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
        if (this.getPageAmount <= this.showPageInPagination) {
          for (let i = 0; i < this.getPageAmount; i++) {
            pages.push(i + 1);
          }
        } else if (this.getPage >= this.getPageAmount - median) {
          for (let i = 0; i < this.showPageInPagination; i++) {
            pages.push(this.getPageAmount - this.showPageInPagination + i + 1);
          }
        } else {
          for (let i = 0; i < this.showPageInPagination; i++) {
            if (this.getPage <= median) pages.push(i + 1);
            else pages.push(i + step);
          }
        }
        return pages;
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
        if (this.searchParams.page < 1) this.searchParams.page = 1;
        this.serchRequest();
      },
      jumpPagesNext() {
        this.searchParams.page = this.getPage + this.showPageInPagination;
        if (this.searchParams.page > this.getPageAmount)
          this.searchParams.page = this.getPageAmount;
        this.serchRequest();
      },
      labelClick() {
        this.allResults = false;
      },
      submit() {
        this.searchParams.page = 1;
        this.serchRequest();
      },
      serchRequest(params = this.generateSearchParams) {
        this.changeUrl(params);
        // uncomit it on develop
        // $.ajax(host + params)
        //   .done((res) => {
        //     this.results = res;
        //     console.log("results", this.results);
        //     this.loading = false;
        //   })
        //   .fail((e) => {
        //     this.loading = false;
        //     throw new Error("Data does not fetch ", e);
        //   });

        // dummy data
        setTimeout(() => {
          const { results, amountResults } = request;
          this.results = results;
          this.amountResults = amountResults;
          this.loading = false;
        }, 500);
      },
      changeUrl(searchParams) {
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
        if (this.getPage !== num) {
          this.searchParams.page = num;
          this.serchRequest();
        }
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
      this.searchParams.queryText = this.getReqValue.queryText;
      this.serchRequest();
    },
  });
};
