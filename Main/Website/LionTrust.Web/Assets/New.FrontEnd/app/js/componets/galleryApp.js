import Vue from "vue/dist/vue.common";
// const eventBus = new Vue();
import {
  baseDownloadChild,
  baseDownloadParent,
} from "./listFilter/mixins/baseDownload";
const data = [
  {
    headshotimage: {
      url: "../images/components/media-gallery/christin-hume.png",
      id: "b7a024e4-5595-475a-b4a1-cfa582f453f9",
    },
    fullbodyimage: null,
    description:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt.",
    id: "e2a024e4-5595-475a-b4a1-cfa532f453f9",
  },
  {
    headshotimage: {
      url: "../images/components/media-gallery/christin-hume2.png",
      id: "b7a024e4-5595-475a-b4a1-cfa582f453f9",
    },
    fullbodyimage: {
      url: "../images/components/media-gallery/christin-hume.png",
      id: "4e17f047-ca05-4491-a0f8-1f87a7f1056a",
    },
    description:
      "John Smith",
    id: "e2a024e4-5595-475a-b4a1-cfa532f453f9",
  },
];

export default () => {
  const mediaItem = Vue.component("media-item", {
    name: "mediaItem",
    props: ["element", "index"],
    mixins: [baseDownloadChild],
    data: function () {
      return {
        selected: false,
      };
    },
    computed: {
      getThumbnailImage() {
        return this.element.images[0];
      },
      getImagesToOverlay() {
        const res = this.element.images.slice(1, this.element.images.length);
        console.log(res);
        return res;
      },
    },
  });

  new Vue({
    el: "#gallery-app",
    components: { "media-item": mediaItem },
    mixins: [baseDownloadParent],
    data: function () {
      return {
        searchText: "",
        items: data,
      };
    },
    methods: {
      downloadDocumentMultiple() {
        console.log("download");
      },
    },
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
