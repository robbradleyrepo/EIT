import Vue from "vue/dist/vue.common";
// const eventBus = new Vue();
import {
  baseDownloadChild,
  baseDownloadParent,
} from "./listFilter/mixins/baseDownload";
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
      {
        thumbnailSrc: "../images/components/media-gallery/christin-hume2.png",
        src: "../images/components/media-gallery/large-image.jpg",
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
        "close"
      ],
    caption(instance, obj) {
       return `<div class="fancy-nav">
            <p>
                <span data-fancybox-index></span>/<span data-fancybox-count></span>
            </p>
            <p>${$(this).data('caption')}</p>
            <a href="${obj.src}" download target="_blank" class="link-styles">
                <i class="icon-download"></i> Download
            </a>
        </div>`;
    },
  });
};
