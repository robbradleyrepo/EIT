import Vue from "vue/dist/vue.common";
// const eventBus = new Vue();
import { baseDownloadChild, baseDownloadParent } from "./listFilter/mixins/baseDownload";
const data = [
  {
    images: [
      {
        thumbnailSrc: "../images/components/media-gallery/christin-hume.png",
        src: "../images/components/media-gallery/christin-hume.png",
      },
      {
        thumbnailSrc: "../images/components/media-gallery/christin-hume2.png",
        src: "../images/components/media-gallery/christin-hume2.png",
      },
    ],
    description:
      "123123Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt.",
    downloadLink: "#some link",
    id: 1,
  },
  {
    images: [
      {
        thumbnailSrc: "../images/components/media-gallery/christin-hume.png",
        src: "../images/components/media-gallery/christin-hume.png",
      },
      {
        thumbnailSrc: "../images/components/media-gallery/christin-hume2.png",
        src: "../images/components/media-gallery/christin-hume2.png",
      },
    ],
    description:
      "333333Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt.",
    downloadLink: "#some link",
    id: 2,
  },
  {
    images: [
      {
        thumbnailSrc: "../images/components/media-gallery/christin-hume.png",
        src: "../images/components/media-gallery/christin-hume.png",
      },
      {
        thumbnailSrc: "../images/components/media-gallery/christin-hume2.png",
        src: "../images/components/media-gallery/christin-hume2.png",
      },
    ],
    description:
      "44444444444Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt.",
    downloadLink: "#some link",
    id: 3,
  },
  {
    images: [
      {
        thumbnailSrc: "../images/components/media-gallery/christin-hume.png",
        src: "../images/components/media-gallery/christin-hume.png",
      },
      {
        thumbnailSrc: "../images/components/media-gallery/christin-hume2.png",
        src: "../images/components/media-gallery/christin-hume2.png",
      },
    ],
    description:
      "555555Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt.",
    downloadLink: "#some link",
    id: 4,
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
    el: "#gallary-app",
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
};
