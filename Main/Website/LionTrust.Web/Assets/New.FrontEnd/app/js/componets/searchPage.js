import Vue from "vue/dist/vue.common.prod";
import {pagination} from "./listFilter/mixins/pagination"
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
    mixins: [pagination],
    data: {
      results: [],
      amountResults: 10,
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
        const entries = Object.entries(this.searchParams);
        entries.forEach(([key, value], index) => {
          if (value) query += `${key}=${value}`;
          if (index !== entries.length - 1) query += "&";
        });
        return query;
      },
      
      getQueryText() {
        return this.searchParams.queryText;
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
