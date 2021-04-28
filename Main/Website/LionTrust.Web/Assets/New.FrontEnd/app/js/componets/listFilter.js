import Vue from "vue/dist/vue.common.prod";

export default () => {
  const host = "http://localhost:3004/article-lister?";
  new Vue({
    el: "#lister-app",
    data: {
      filters: {
        fund: {
          name: "Funds",
          values: [
            { name: "James James Inglis-Jones", checked: false },
            { name: "Samantha Gleave", checked: false },
            { name: "Samantha Gleave 123", checked: false },
            { name: "Test 34", checked: false },
            { name: "12323123", checked: false },
            { name: "432 432 4", checked: false },
          ],
        },
        fundTeam: {
          name: "Funds",
          values: [
            { name: "James James Inglis-Jones", checked: false },
            { name: "Samantha Gleave", checked: false },
            { name: "Samantha Gleave 123", checked: false },
            { name: "Test 34", checked: false },
            { name: "12323123", checked: false },
            { name: "432 432 4", checked: false },
          ],
        },
        fundManager: {
          name: "Funds",
          values: [
            { name: "James James Inglis-Jones", checked: false },
            { name: "Samantha Gleave", checked: false },
            { name: "Samantha Gleave 123", checked: false },
            { name: "Test 34", checked: false },
            { name: "12323123", checked: false },
            { name: "432 432 4", checked: false },
          ],
        },
        category: {
          name: "Funds",
          values: [
            { name: "James James Inglis-Jones", checked: false },
            { name: "Samantha Gleave", checked: false },
            { name: "Samantha Gleave 123", checked: false },
            { name: "Test 34", checked: false },
            { name: "12323123", checked: false },
            { name: "432 432 4", checked: false },
          ],
        },
      },
      month: 0,
      year: 0,
    },
  });
};
