import Vue from "vue/dist/vue.common";
import { API } from "./listFilter/api/api";
import { eventBus } from "./listFilter/bus";

import {
  baseDownloadChild,
  baseDownloadParent,
} from "./listFilter/mixins/baseDownload";
const data = [
  {
    headshotimage: {
      url: "../images/components/media-gallery/christin-hume.png",
      id: "b7a02424-5595-475a-b4a1-cfa582f453f9",
    },
    fullbodyimage: null,
    description:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt.",
    id: "e2a024e4-5595-475a-b4a1-cfa532f453f9",
  },
  {
    headshotimage: {
      url: "../images/components/media-gallery/christin-hume2.png",
      id: "b7a024e4-5593-475a-b4a1-cfa582f453f9",
    },
    fullbodyimage: {
      url: "../images/components/media-gallery/christin-hume.png",
      id: "4e17f047-ca05-4491-a0f5-1f87a7f1056a",
    },
    description: "John Smith",
    id: "e2a024e1-5595-475a-b2a1-cfa532f453f9",
  },
];

const facets = [
  {
    identifier: "c8e97f824fb246d3b1628fa1fb9fe0f2",
    name: "Fund",
  },
  {
    identifier: "c8e97f824fb246d3b1628fa1fb9fe0f3",
    name: "Manager",
  },
  {
    identifier: "c8e97f824fb246d3b1628fa1fb9fe0f4",
    name: "Article",
  },
  {
    identifier: "c8e97f824fb246d3b1628fa1fb9fe0f5",
    name: "Fund detail",
  },
];

export default () => {
  const selectField = Vue.component("select-field", {
    name: "selectField",
    data: () => ({
      selected: false,
    }),
    methods: {
      onChange() {
        this.$emit("on-check");
      },
    },
    created() {
      eventBus.$on("toggleSelected", (selected) => {
        this.selected = selected;
        if (selected) this.onChange();
      });
    },
    beforeDestroy() {
      eventBus.$off("toggleSelected");
    },
  });

  const mediaItem = Vue.component("media-item", {
    name: "mediaItem",
    components: { selectField },
    props: ["element", "index"],
    mixins: [baseDownloadChild],
    data: () => ({
      ids: [],
    }),
    computed: {
      getImagesIds() {
        if (this.ids.length) return this.ids;
        const arr = [];
        arr.push(this.element.headshotimage.id);
        if (this.element.fullbodyimage) arr.push(this.element.fullbodyimage.id);
        return arr;
      },
    },
    methods: {
      selectDocument(id) {
        this.$parent.setDocumentId(id);
        const index = this.ids.findIndex((el) => el === id);
        if (index !== -1) this.ids.splice(index, 1);
        else this.ids.push(id);
      },
      download() {
        // API.downloadDocument('', {downloadFileIds: this.getImagesIds})
      },
    },
    created() {
      console.log("this.element", this.element);
    },
  });

  new Vue({
    el: "#gallery-app",
    components: { "media-item": mediaItem },
    mixins: [baseDownloadParent],
    data: function () {
      return {
        searchText: "",
        loading: false,
        items: data,
        facets: facets,
        filter: "",
      };
    },
    computed: {
      getQuery() {
        let str = "";
        if (this.searchText) str += "searchTerm=" + this.searchText;
        if (this.filter && this.searchText) str += "&filter=" + this.filter;
        if (this.filter) str += "filter=" + this.filter;
        return str;
      },
    },
    methods: {
      setFilter(e) {
        this.filter = e.target.value;
        this.getData();
      },
      submitSearchForm() {
        this.getData();
      },
      downloadDocumentMultiple() {
        console.log("download");
        // API.downloadDocument('', {downloadFileIds: this.selectedDocumentIds})
      },
      getData() {
        console.log("getData");
        this.loading = true;
        setTimeout(() => {
          this.loading = false
        }, 1500);
        return;
        this.loading = true;
        let hostUrl = "";
        $.get(hostUrl + this.getQuery)
          .done((response) => {
            const { searchResults } = response;
            this.items = searchResults;
            this.loading = false;
          })
          .fail((e) => {
            console.error(e);
            this.loading = false;
          });
      },
    },
    mounted() {
      this.getData();
    }
  });

  // init fancybox image carousel
  $(".media-item-image-gallary").fancybox({
    infobar: false,
    buttons: [
      // "zoom",
      //"share",
      // "slideShow",
      //"fullScreen",
      //"download",
      // "thumbs",
      "close",
    ],
    caption(instance, obj) {
      return `<div class="fancy-nav">
            <p>
                <span data-fancybox-index></span>/<span data-fancybox-count></span>
            </p>
            <p>${$(this).data("caption")}</p>
            <a href="${obj.src}" download target="_blank" class="link-styles">
                <i class="icon-download"></i> Download
            </a>
        </div>`;
    },
  });
};
