import Vue from "vue/dist/vue.common.prod";
import { pagination } from "./listFilter/mixins/pagination";
export default () => {
  const host = "https://cm-liontrust-it.sagittarius.agency/ArticleSearchApi/";
  const months = ['January','February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  const responce =  {"SearchResults":[{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":null,"ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"The Multi-Asset Process","Subtitle":"August 2017 market review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"GF Special Situations Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust GF Special Situations Fund ","Subtitle":"April 2018 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"post","Content":null,"Date":"Monday, January 1, 0001","Fund":null,"ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Investing in travel’s Silver Bullet","Subtitle":"Neil Brown","Topics":null},{"Authors":["Matt Tonge"],"AuthorImageUrl":null,"Category":"post","Content":null,"Date":"Monday, January 1, 0001","Fund":null,"ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"3 success stories and 3 to watch","Subtitle":null,"Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"Balanced Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust Balanced Fund ","Subtitle":"Q3 2019 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"GF Sustainable Future Pan-European Growth Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust GF SF Pan-European Growth Fund ","Subtitle":"Q3 2019 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"European Growth Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust European Growth Fund","Subtitle":"January 2018 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"UK Micro Cap Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust UK Micro Cap Fund","Subtitle":"January 2018 review","Topics":null},{"Authors":["David Roberts"],"AuthorImageUrl":null,"Category":"post","Content":null,"Date":"Monday, January 1, 0001","Fund":null,"ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Why markets have been pitched a curveball","Subtitle":null,"Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"Global Income Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust Global Income Fund","Subtitle":"January 2018 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"Sustainable Future European Growth Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust SF European Growth Fund","Subtitle":"Q4 2017 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"Sustainable Future Corporate Bond Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust SF Corporate Bond Fund","Subtitle":"Q4 2017 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"GF UK Growth Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust GF UK Growth Fund ","Subtitle":"August 2020 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"Sustainable Future Corporate Bond Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust SF Corporate Bond Fund ","Subtitle":"Q1 2019 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"UK Smaller Companies Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust UK Smaller Companies Fund","Subtitle":"December 2017 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"Sustainable Future Defensive Managed Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust SF Defensive Managed Fund","Subtitle":"Q4 2017 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"Sustainable Future Managed Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust SF Managed Fund","Subtitle":"Q4 2017 review","Topics":null},{"Authors":["Peter Michaelis"],"AuthorImageUrl":null,"Category":"post","Content":null,"Date":"Monday, January 1, 0001","Fund":null,"ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Green ‘industrial revolution’ highlights non-linear nature of change","Subtitle":null,"Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"review","Content":null,"Date":"Monday, January 1, 0001","Fund":"Sustainable Future Global Growth Fund","ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"Liontrust SF Global Growth Fund","Subtitle":"Q4 2017 review","Topics":null},{"Authors":null,"AuthorImageUrl":null,"Category":"post","Content":null,"Date":"Wednesday, June 9, 2021","Fund":null,"ImageUrl":"/sitecore/shell/-/media/liontrust/images/site-sections/benefits-of-investing/quilt-3.ashx?db=web&hash=FDF80C6A3D1BB4548E77B2333B825E20","Team":null,"Title":"Reasons to be cheerful in 2018","Subtitle":"John Husselbee, Olly Russ, Jamie Clark, Mark Williams, Anthony Cross, Peter Michaelis & James Inglis-Jones","Topics":["Topic 1","Topic 2"]},{"Authors":null,"AuthorImageUrl":null,"Category":"post","Content":null,"Date":"Monday, January 1, 0001","Fund":null,"ImageUrl":"/sitecore/shell/-/media/foundation/indexing/default-article-listing-image.ashx?db=web&hash=83D55F43BC1D20A4264D327695971205","Team":null,"Title":"2018: what should investors expect this year?","Subtitle":"Anthony Cross, Carolyn Chan, Neil Brown, John Husselbee, Olly Russ, Samantha Gleave & Stephen Bailey","Topics":null}],"StatusCode":200,"StatusMessage":"Success","TotalResults":1179}
  const facets = [{"name":"Funds","data":[{"Name":"Shared documents","Identifier":"c8e97f824fb246d3b1628fa1fb9fe0ff"},{"Name":"Asia Income Fund","Identifier":"b0c7ac70389549c48f3c4df11efedc46"},{"Name":"Balanced Fund","Identifier":"57693a7660514fe0af4c1ca2c87f2989"},{"Name":"China Fund","Identifier":"17361a1640b046fe9ad693345775ba8d"},{"Name":"Emerging Markets Fund","Identifier":"cb61f0f7dd8c401fbab954f13908e208"},{"Name":"European Enhanced Income Fund","Identifier":"8bf2aab737b446c09599d4fb3530709d"},{"Name":"European Growth Fund","Identifier":"a4c505f97bf347518f9767fad4f3668e"},{"Name":"European Income Fund","Identifier":"060aa0c3cdbc47dda43ee3456e72e443"},{"Name":"European Opportunities Fund","Identifier":"1401a2d886a449329d8f487256dc3b0d"},{"Name":"GF Absolute Return Bond Fund","Identifier":"068227ab8def4a609c7bd34739b4fb39"},{"Name":"GF European Smaller Companies Fund","Identifier":"dd1fb6fb0e7044878e77c985dae34407"},{"Name":"GF European Strategic Equity Fund","Identifier":"aad03e4bcd824df0bb19ad4f43706fa3"},{"Name":"GF High Yield Bond Fund","Identifier":"a222ecbf74424884b13b3fbfea108a77"},{"Name":"GF Special Situations Fund","Identifier":"6ec98d5fb68e4e8996bae3dc283f1b56"},{"Name":"GF Strategic Bond Fund","Identifier":"db186d8e97d44bf7b87ceab6c44c21bc"},{"Name":"GF Sustainable Future European Corporate Bond Fund","Identifier":"f0851e0c4f1a4a72a58b867d503cc93b"},{"Name":"GF Sustainable Future Global Growth Fund","Identifier":"83bdac7d3e5142b28f2b71e0258485c6"},{"Name":"GF Sustainable Future Pan-European Growth Fund","Identifier":"cab2fb236928475ab0cfea5ba70ee2ca"},{"Name":"GF UK Growth Fund","Identifier":"2335db68ad954e4eb11b007605557258"},{"Name":"Global Alpha Fund","Identifier":"b1d4f3a2cd2f4cefa12b2431a68b569e"},{"Name":"Global Dividend Fund","Identifier":"69c07e6b044d4bbe8b6eaaee9291d9ed"},{"Name":"Global Equity Fund","Identifier":"c6c384c40caa4debb452f24c8d8b4e61"},{"Name":"Global Income Fund","Identifier":"afb533f288a348a8bd79b76c1367ea0f"},{"Name":"Global Smaller Companies Fund","Identifier":"4854409cecdb40ceb6390c191fd9e55e"},{"Name":"Global Technology Fund","Identifier":"0704e26f28d4411d853adbd326b5379b"},{"Name":"Income Fund","Identifier":"80bfaa5accd94d0a98a93f78b62488aa"},{"Name":"India Fund","Identifier":"bee76f7087494152976aa56c67512ce9"},{"Name":"Japan Equity Fund","Identifier":"bada89cd3a864dcba8ecb89d37e9630f"},{"Name":"Japan Opportunities Fund","Identifier":"ff40a417f3cf4fb1b9268f53dd39cee7"},{"Name":"Latin America Fund","Identifier":"d335d329d8684ed094d136aa958dfd5d"},{"Name":"MA Active Dynamic Fund","Identifier":"b9a42501da02419fba3422fe30083a39"},{"Name":"MA Active Growth Fund","Identifier":"e538c5b92b09432a90c1bc434072d86d"},{"Name":"MA Active Intermediate Income Fund","Identifier":"b7ae47982fd4464eb232bfcfdfe1aa11"},{"Name":"MA Active Moderate Income Fund","Identifier":"fea12e787cf843cea34dd34a09e0c6a3"},{"Name":"MA Active Progressive Fund","Identifier":"8660503aaa7848719783a84beac9954a"},{"Name":"MA Active Reserve Fund","Identifier":"ec799242444842ac8542def9942ded49"},{"Name":"MA Birthstar TD 2015-20 Fund","Identifier":"ded0278bcfb14413a6f24f174c9150f9"},{"Name":"MA Birthstar TD 2021-25 Fund","Identifier":"33ba2f1ab9d04b648e23e13660fab133"},{"Name":"MA Birthstar TD 2026-30 Fund","Identifier":"2a768cdc92554936b645b90c33ef23e0"},{"Name":"MA Birthstar TD 2031-35 Fund","Identifier":"db57b1b4f59a40ba9b1c12231b346025"},{"Name":"MA Birthstar TD 2036-40 Fund","Identifier":"b56d00fc48194eedab6b38d6718ef6d0"},{"Name":"MA Birthstar TD 2041-45 Fund","Identifier":"586753e03b91441fbc9304eb59b4ab8b"},{"Name":"MA Birthstar TD 2046-50 Fund","Identifier":"b4300207108345a493d54f1ddf096ebe"},{"Name":"MA Blended Growth Fund","Identifier":"5b826ccfd4354c8696e904daa99e890f"},{"Name":"MA Blended Intermediate Fund","Identifier":"dad47e770c7341b08a0e53814473f044"},{"Name":"MA Blended Moderate Fund","Identifier":"6d0a84a1087141afbb845f8e1840a271"},{"Name":"MA Blended Progressive Fund","Identifier":"81b1b8f302fa46b1be9c4a7f015567ed"},{"Name":"MA Blended Reserve Fund","Identifier":"357bb108dd2e4e5dabe2f62f377b337a"},{"Name":"MA Diversified Global Income Fund","Identifier":"7683545c4a4d431da630c3c426fcd6f1"},{"Name":"MA Diversified Protector 70 Fund","Identifier":"dbc5abe6af264192bcbe67255ac54874"},{"Name":"MA Diversified Protector 80 Fund","Identifier":"ddff900252db4431ba40aaa9c3fa735f"},{"Name":"MA Diversified Protector 85 Fund","Identifier":"1c9e6800d78642d28dfe47e2c7ac447c"},{"Name":"MA Diversified Real Assets Fund","Identifier":"a91ebfa90d3a42008860c0f4926af8f4"},{"Name":"MA Global Equity Income Fund","Identifier":"a6734482c34346fc8af0cd60ce244f6f"},{"Name":"MA Monthly High Income Fund","Identifier":"fe6aa79b1d114b78acfa6ea1464e318e"},{"Name":"MA Passive Dynamic Fund","Identifier":"4e984ef9a0364fe0ae2a2b0e3bb6acca"},{"Name":"MA Passive Growth Fund","Identifier":"f3a1c7a71d1f4a0e88a35655e1105211"},{"Name":"MA Passive Intermediate Fund","Identifier":"d8c813ea12c647019b15b0e49b259ce7"},{"Name":"MA Passive Moderate Fund","Identifier":"2919c4e91c2443b184a552d5dc7ab144"},{"Name":"MA Passive Progressive Fund","Identifier":"542882eb1cb24762a55f5b5dba2023e1"},{"Name":"MA Passive Prudent Fund","Identifier":"149b78664abe447ba840732549e781bb"},{"Name":"MA Passive Reserve Fund","Identifier":"1da7adfe69dd4ad3a3d782623fd9466d"},{"Name":"MA Positive Future Fund","Identifier":"d3a7bdaa8eb841349f9aff1b973ad6f2"},{"Name":"MA Strategic Bond Fund","Identifier":"9ae4e6605c1444cf949b13c4700132ae"},{"Name":"MA UK Equity Fund","Identifier":"29adabd704ae4a1bb2cf6aea256e119b"},{"Name":"Macro Equity Income Fund","Identifier":"35a8464ca192460ea3d7633cd35aa012"},{"Name":"Macro UK Growth Fund","Identifier":"de369ff5d1e84d5e9b072ed104d1930d"},{"Name":"Monthly Income Bond Fund","Identifier":"dd0f569785a74dcdb62c649585c8423b"},{"Name":"Russia Fund","Identifier":"2f6d6db81db84634ae7e85016520ce05"},{"Name":"Special Situations Fund","Identifier":"f2f17a8fdec8458d965a1f53aae9af6d"},{"Name":"Strategic Bond Fund","Identifier":"fbb00f95750b4d7a8fb5c032f454b376"},{"Name":"Sustainable Future Cautious Managed Fund","Identifier":"ad3bb77cd594422f81bb9b6856d8953f"},{"Name":"Sustainable Future Corporate Bond Fund","Identifier":"478c9cbcc7364a04b555e65998a64c16"},{"Name":"Sustainable Future Defensive Managed Fund","Identifier":"eef5c9fd444c4c6895446db766182f61"},{"Name":"Sustainable Future European Growth Fund","Identifier":"cdf0e61f2535448691836a093254a7e9"},{"Name":"Sustainable Future Global Growth Fund","Identifier":"c2095fe1d6b74592aae7c46994e21d3b"},{"Name":"Sustainable Future Managed Fund","Identifier":"01013679a90f4c97b7340b4cb72bef56"},{"Name":"Sustainable Future Managed Growth Fund","Identifier":"fc89cac102614e999c79956e34a50251"},{"Name":"Sustainable Future UK Growth Fund","Identifier":"6cb4e6e087644a8a9155153e5db4738a"},{"Name":"UK Ethical Fund","Identifier":"8fe8f95a86094c3eba69bcf56a8017f4"},{"Name":"UK Growth Fund","Identifier":"d8f81e1bee414385b5f4c30a63b6b6ca"},{"Name":"UK Micro Cap Fund","Identifier":"2f0ad87983d744caa9e4bd72c73f4d72"},{"Name":"UK Mid Cap Fund","Identifier":"c53d76fddc8a4df9a8ebb413e1917b26"},{"Name":"UK Opportunities Fund","Identifier":"d576bba7b95340459127904fe9d3b133"},{"Name":"UK Smaller Companies Fund","Identifier":"c553bba1c26e43fb98aae0163f8c54de"},{"Name":"US Income Fund","Identifier":"748f38d717df416199df750d2ad48fd8"},{"Name":"US Opportunities Fund","Identifier":"14bc0bf0fcd948678421cee4ff894e79"},{"Name":"Benchmarks","Identifier":"c9224be2659e4f1881f590991efd1f3d"}]},{"name":"Fund Categories","data":[{"Name":"Fund manager views","Identifier":"a0938a5bece447d88564421c0d816141"},{"Name":"Fund updates","Identifier":"d0700e768bc2427a9849fe3b6d28bd22"},{"Name":"Magazine and Reports","Identifier":"ffdb4296adc242c4b0ddc738e4db1441"},{"Name":"Podcast","Identifier":"74a073053f2447669595ad788cad354a"},{"Name":"Video","Identifier":"4cbe4422dcf04ffe8064bb7cad356e7d"}]},{"name":"Fund Managers","data":[{"Name":"Aitken Ross","Identifier":"bd0f07d08cc041e581053ba43c9e9dc8"},{"Name":"Alex Wedge","Identifier":"039dd4f18cee408ba20693acb8b512d6"}]},{"name":"Fund Teams","data":[{"Name":"The Asia Team","Identifier":"f1bf43b032024614a5ddd26d01ad8c10"},{"Name":"The Cashflow Solution team","Identifier":"7b3704359ab24e6f9b1c16209ba9c8bd"},{"Name":"The Economic Advantage Team","Identifier":"ff63c615a3f4496fa0f26d72f92e9ba9"},{"Name":"The Global Equity Team","Identifier":"7dd7ce76ef32471fb07579efff994691"},{"Name":"The Global Fixed Income Team","Identifier":"37e9b54e1d5443daa6dff85ed1a13531"},{"Name":"The Multi-Asset Team","Identifier":"070e5c0b03b244389de1555ee1b65dd6"},{"Name":"The Sustainable Investment Team","Identifier":"64900d7a93a64b31a639e1ba6e52d5b4"}]}]
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
      years: []
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
        // this.pushStateLink();        
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
        if(e.target.searchText.value)
          this.applyFilters()
      },

      getFacetsRequest() {
        $.get(
          `${host}Facets`
        ).done((responce) => {
          const {Facets, Dates} = responce;
          const facets = [];
          for (let i in Facets) {
            const name = i.replace(/([a-z])([A-Z])/g, "$1 $2");
            facets.push({
              name,
              data: Facets[i],
            });
          }
          this.facets = facets;
          console.log('this.facets',JSON.stringify(this.facets));

          if(Dates.Months.length)
            for(let i in Dates.Months) {
              this.months.push(months[i])
            }
          if(Dates.Years.length)
            this.years = Dates.Years;

        }).fail(e => {
          console.error(e);
          this.loading = false
        })
      },

      getSearchRequest() {
        this.loading = true;
        $.get(
          host + "Search?" +
            this.getQuerySring()
        ).done((responce) => {
          console.log('responce',JSON.stringify(responce));
          const { SearchResults, TotalResults } = responce;
          this.searchData = SearchResults;
          this.amountResults = TotalResults;
          this.loading = false;          
        })
        .fail(e => {
          console.error(e);
          this.loading = false
        })
      }
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

Vue.component("fund-item", {
  data: function () {
    return {};
  },
});
