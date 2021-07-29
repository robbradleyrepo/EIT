import Vue from "vue/dist/vue.common.prod";
export default () => {
  new Vue({
    el: "#gallary-app",
    data: {
      searchText: "",
    },
  });
};
