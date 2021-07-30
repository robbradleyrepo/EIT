import { eventBus } from "../bus";


// mixin for downloading functional for parent component
export const baseDownloadParent = {
  data: function () {
    return {
      selectedDocumentIds: [],
      selectAllDocuments: false,
    };
  },
  methods: {
    setDocumentId(id) {
      const index = this.selectedDocumentIds.findIndex((el) => el === id);
      if (index !== -1) this.selectedDocumentIds.splice(index, 1);
      else this.selectedDocumentIds.push(id);
    },
  },
  watch: {
    selectAllDocuments: function (value) {
      this.selectedDocumentIds = [];
      eventBus.$emit("toggleSelected", value);
    },
  },
};

// mixin for download functional for chind component
export const baseDownloadChild = {
  methods: {
    selectDocument(id) {
      this.$parent.setDocumentId(id);
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
};
