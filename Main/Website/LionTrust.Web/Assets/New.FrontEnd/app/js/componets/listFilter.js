import Vue from "vue/dist/vue.common.prod";

const facets = {
  FundFacets: {open: false,
    name: 'Fund',
    data: [      
      { Name: "Russia Fund",  checked: false,
      Identifier: "2f6d6db8-1db8-4634-ae7e-85016520ce05" },
      {
        Name: "Special Situations Fund",
        checked: false,
        Identifier: "f2f17a8f-dec8-458d-965a-1f53aae9af6d",
      },
      {
        Name: "Strategic Bond Fund",
        checked: false,
        Identifier: "fbb00f95-750b-4d7a-8fb5-c032f454b376",
      },
      {
        Name: "Sustainable Future Cautious Managed Fund",
        checked: false,
        Identifier: "ad3bb77c-d594-422f-81bb-9b6856d8953f",
      },
      {
        Name: "Sustainable Future Corporate Bond Fund",
        checked: false,
        Identifier: "478c9cbc-c736-4a04-b555-e65998a64c16",
      },
      {
        Name: "Sustainable Future Defensive Managed Fund",
        checked: false,
        Identifier: "eef5c9fd-444c-4c68-9544-6db766182f61",
      },
      {
        Name: "Sustainable Future European Growth Fund",
        checked: false,
        Identifier: "cdf0e61f-2535-4486-9183-6a093254a7e9",
      },
      {
        Name: "Sustainable Future Global Growth Fund",
        checked: false,
        Identifier: "c2095fe1-d6b7-4592-aae7-c46994e21d3b",
      },
      {
        Name: "Sustainable Future Managed Fund",
        checked: false,
        Identifier: "01013679-a90f-4c97-b734-0b4cb72bef56",
      },
      {
        Name: "Sustainable Future Managed Growth Fund",
        checked: false,
        Identifier: "fc89cac1-0261-4e99-9c79-956e34a50251",
      },
      {
        Name: "Sustainable Future UK Growth Fund",
        checked: false,
        Identifier: "6cb4e6e0-8764-4a8a-9155-153e5db4738a",
      },
      {
        Name: "UK Ethical Fund",
        checked: false,
        Identifier: "8fe8f95a-8609-4c3e-ba69-bcf56a8017f4",
      },
      {
        Name: "UK Growth Fund",
        checked: false,
        Identifier: "d8f81e1b-ee41-4385-b5f4-c30a63b6b6ca",
      },
      {
        Name: "UK Micro Cap Fund",
        checked: false,
        Identifier: "2f0ad879-83d7-44ca-a9e4-bd72c73f4d72",
      },
      {
        Name: "UK Mid Cap Fund",
        checked: false,
        Identifier: "c53d76fd-dc8a-4df9-a8eb-b413e1917b26",
      },
      {
        Name: "UK Opportunities Fund",
        checked: false,
        Identifier: "d576bba7-b953-4045-9127-904fe9d3b133",
      },
      {
        Name: "UK Smaller Companies Fund",
        checked: false,
        Identifier: "c553bba1-c26e-43fb-98aa-e0163f8c54de",
      },
      {
        Name: "US Income Fund",
        checked: false,
        Identifier: "748f38d7-17df-4161-99df-750d2ad48fd8",
      },
      {
        Name: "US Opportunities Fund",
        checked: false,
        Identifier: "14bc0bf0-fcd9-4867-8421-cee4ff894e79",
      },
      { Name: "Benchmarks", checked: false,
      Identifier: "c9224be2-659e-4f18-81f5-90991efd1f3d" },
    ]
  },
  FundCategoriesFacets: {
    open: false,
    name: 'Fund Category',
    data: [{
      Name: "Fund manager views",
      checked: false,
      Identifier: "a0938a5b-ece4-47d8-8564-421c0d816141",
    },
    {
      Name: "Fund updates",
      checked: false,
      Identifier: "d0700e76-8bc2-427a-9849-fe3b6d28bd22",
    },
    {
      Name: "Magazine and Reports",
      checked: false,
      Identifier: "ffdb4296-adc2-42c4-b0dd-c738e4db1441",
    },
    { Name: "Podcast", checked: false,
    Identifier: "74a07305-3f24-4766-9595-ad788cad354a" },
    { Name: "Video", checked: false,
    Identifier: "4cbe4422-dcf0-4ffe-8064-bb7cad356e7d" },]
  },
  FundManagersFacets: {
    open: false,
    name: 'Fund Managers',
    data: [
      
    { Name: "Aitken Ross", checked: false,
    Identifier: "bd0f07d0-8cc0-41e5-8105-3ba43c9e9dc8" },
    { Name: "Alex Wedge", checked: false,
    Identifier: "039dd4f1-8cee-408b-a206-93acb8b512d6" },
    ]
  },
  FundTeamsFacets: {
    open: false,
    name: 'Fund Teams',
    data: [
      {
        Name: "The Asia Team",
        checked: false,
        Identifier: "f1bf43b0-3202-4614-a5dd-d26d01ad8c10",
      },
      {
        Name: "The Cashflow Solution team",
        checked: false,
        Identifier: "7b370435-9ab2-4e6f-9b1c-16209ba9c8bd",
      },
      {
        Name: "The Economic Advantage Team",
        checked: false,
        Identifier: "ff63c615-a3f4-496f-a0f2-6d72f92e9ba9",
      },
      {
        Name: "The Global Equity Team",
        checked: false,
        Identifier: "7dd7ce76-ef32-471f-b075-79efff994691",
      },
      {
        Name: "The Global Fixed Income Team",
        checked: false,
        Identifier: "37e9b54e-1d54-43da-a6df-f85ed1a13531",
      },
      {
        Name: "The Multi-Asset Team",
        checked: false,
        Identifier: "070e5c0b-03b2-4438-9de1-555ee1b65dd6",
      },
      {
        Name: "The Sustainable Investment Team",
        checked: false,
        Identifier: "64900d7a-93a6-4b31-a639-e1ba6e52d5b4",
      },
    ]
  },
  Message: null,
  StatusCode: 0,
};

export default () => {
  const host = "http://localhost:3004/article-lister?";
  new Vue({
    el: "#lister-app",
    data: {
     facets: {}, 
     params: {},
     page: 1
    },
    computed: {
      getFacets() {
        const res = {}
        for(let i in this.facets) {
          if( i != 'Message' && i != 'StatusCode')
            res[i] = this.facets[i]
        }
        return res
      },
      getQuerySring() {
        let str = '';
        str = str + 'page=' + this.page;
        for(let prop in this.params) {
          console.log('prop',prop);
          str += '&' +`${prop}=${this.params[prop]}` 
        }
        console.log('str', str);
        return str
      }
    },
    methods: {
      // adding selected values to query params
      toggleSelect(item, facet) {
        // const test = this.queryValues.params[facet.name]
        if(!this.params[facet.name])
        this.params[facet.name] = [];
        const existElem = this.params[facet.name].findIndex((el) => {
          return  el === item.Identifier
        }) 

        console.log('existElem',existElem);
        if(existElem !== - 1)
          this.params[facet.name].splice(existElem, 1)
        else        
          this.params[facet.name].push(item.Identifier);
        console.log('this.params',this);        
       },
       getQuerySring() {
        let str = '';
        str = str + 'page=' + this.page;
        for(let prop in this.params) {
          console.log('prop',prop);
          str += '&' +`${prop}=${this.params[prop]}` 
        }
        console.log('str', str);
        return str
      },
       applyFilters() {
          
          window.history.pushState(
            { page: "search-page" },
            "search",
            `${window.location.href.split("?")[0]}?${this.getQuerySring()}`
          );
       },
       clearFilters() {
         this.queryValues
       }
    },
    // watch: {
    //   facets: {
    //     handler: (newVal, oldVal) => {
    //       console.log('newVal, oldVal',newVal, oldVal);
    //     },
    //     deep: true
    //   }
    // },
    mounted() {
      // const req = $.ajax(
      //   "https://cm-liontrust-it.sagittarius.agency/ArticleSearchApi/Facets"
      // );
      // console.log("req", req);
      this.facets = facets
      console.log('this.facets',this.facets);
    },
  });
};
