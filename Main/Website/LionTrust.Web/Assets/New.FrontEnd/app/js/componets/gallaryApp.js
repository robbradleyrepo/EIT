import Vue from "vue/dist/vue.common";
const eventBus = new Vue();
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
    methods: {
      selectDocument() {
        this.$parent.setDocumentId(this.element.id);
      },
    },
    created() {
      eventBus.$on("toggleSelected", (selected) => {
        this.selected = selected;
        if (selected) this.selectDocument(this.id);
      });
    },
    beforeDestroy() {
      eventBus.$off("toggleSelected");
    },
  });

  new Vue({
    el: "#gallary-app",
    components: { "media-item": mediaItem },
    data: {
      searchText: "",
      items: data,
      selectedDocumentIds: [],
      selectAllDocuments: false
    },
    methods: {
      setDocumentId(id) {
        console.log("id", id);
        const index = this.selectedDocumentIds.findIndex((el) => el === id);
        if (index !== -1) this.selectedDocumentIds.splice(index, 1);
        else this.selectedDocumentIds.push(id);
      },
      downloadDocumentMultiple() {
          console.log('download');
      }
    },
    watch: {
      selectAllDocuments: function (value) {
        this.selectedDocumentIds = [];
        eventBus.$emit("toggleSelected", value);
      },
    },
  });
};
