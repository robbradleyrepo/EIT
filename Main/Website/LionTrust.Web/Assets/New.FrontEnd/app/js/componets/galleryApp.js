import Vue from "vue/dist/vue.common";
import { API } from "./listFilter/api/api";
import { eventBus } from "./listFilter/bus";

import {
    baseDownloadChild,
    baseDownloadParent,
} from "./listFilter/mixins/baseDownload";

var rootDom = document.getElementById("gallery-app");
if (rootDom != null) {
    var mediaGalleryId = rootDom?.dataset?.galleryid;
    var host = rootDom?.dataset?.host;

    var location = window.location.hostname;
    let root = "";
    if (
        window.location.hostname === "localhost" ||
        window.location.hostname === "127.0.0.1"
    ) {
        host = location + host;
        root = location;
    } else {
        host = "/" + host;
    }
}


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
                arr.push(this.element.headShotImage.id);
                if (this.element.fullBodyImage) arr.push(this.element.fullBodyImage.id);
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
            downloadDocument() {
                API.downloadDocument(
                    `${host}/DownloadMediaImages`, {
                        downloadMediaIds: this.getImagesIds,
                    },
                    "images"
                );
            },
        },
    });

    const optionField = Vue.component("option-field", {
        name: "option-field",
        props: ["facet"],
        data: () => ({
            active: 0,
            name: "All",
        }),
        methods: {
            selectFacet(facetid) {
                eventBus.$emit("setFaccets", facetid);
                this.$emit("closetab");
            },
        },
    });

    const seclectField = Vue.component("select-field", {
        name: "select-field",
        components: { optionField },
        data: () => ({
            init: true,
            open: false,
            active: false,
            facets: [],
        }),
        methods: {
            toggleOption() {
                this.open = !this.open;
            },
            getFacets() {
                $.get(`${host}/GetMediaFacets?mediaGalleryId={${mediaGalleryId}}`)
                    .done((response) => {
                        this.facets = response;
                    })
                    .fail((e) => {
                        console.error(e);
                    });
            },
        },
        mounted() {
            this.getFacets();
            document.querySelector("body").addEventListener("click", () => {
                this.open = false;
            });
        },
    });

    new Vue({
        el: rootDom,
        components: { mediaItem, seclectField },
        mixins: [baseDownloadParent],
        data: function() {
            return {
                searchText: "",
                loading: false,
                items: [],
                filter: "",
            };
        },
        computed: {
            getQuery() {
                let str = "";
                if (this.searchText) str += "searchTerm=" + this.searchText;
                if (this.filter && this.searchText) str += "&filter=" + this.filter;
                if (this.filter) str += "&filter=" + this.filter;
                return str;
            },
        },
        methods: {
            setFilter(id) {
                this.filter = id;
                this.getData();
            },
            submitSearchForm() {
                this.getData();
            },
            downloadDocumentMultiple() {
                API.downloadDocument(
                    `${host}/DownloadMediaImages`, {
                        downloadMediaIds: this.selectedDocumentIds,
                    },
                    "images"
                );
            },
            getData() {
                this.loading = true;
                $.get(
                        `${host}/GetmediaItems?mediaGalleryId=${mediaGalleryId}${this.getQuery}`
                    )
                    .done((response) => {
                        const { searchResults } = response;
                        this.items = searchResults;
                        this.loading = false;
                    })
                    .fail((e) => {
                        console.error(e);
                        this.loading = false;
                        this.items = [];
                    });
            },
        },
        mounted() {
            this.getData();
        },
        created() {
            eventBus.$on("setFaccets", (id) => {
                this.setFilter(id);
            });
        },
        beforeDestroy() {
            eventBus.$off("setFaccets");
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