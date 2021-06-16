export const pagination = {
  computed: {
    getPage() {
      return this.page;
    },
    getPageAmount() {
      return Math.floor(this.amountResults / this.showPerPage);
    },
    showPages() {
      return this.getPage;
    },
    getPages() {
      const pages = [];
      const median = Math.floor(this.showPageInPagination / 2); // 3
      const step = this.getPage - median; // 5 - 4 = 1
      if (this.getPageAmount <= this.showPageInPagination) {
        for (let i = 0; i < this.getPageAmount; i++) {
          pages.push(i + 1);
        }
      } else if (this.getPage >= this.getPageAmount - median) {
        for (let i = 0; i < this.showPageInPagination; i++) {
          pages.push(this.getPageAmount - this.showPageInPagination + i + 1);
        }
      } else {
        for (let i = 0; i < this.showPageInPagination; i++) {
          if (this.getPage <= median) pages.push(i + 1);
          else pages.push(i + step);
        }
      }
      console.log('pages',pages);
      return pages;
    },
    showForwardPageBtn() {
      return !this.getPages.includes(1);
    },
    showNextPageBtn() {
      return !this.getPages.includes(this.getPageAmount);
    },
    disablePrevBtn() {
      return this.getPage === 1;
    },
    disabledNextBtn() {
      return this.getPage === this.getPageAmount;
    },
    getFilterTopPosition() {
      return document.getElementById('lister-app').offsetTop;
    }
  },

  methods: {
    jumpPagesForward() {
      this.page = this.getPage - this.showPageInPagination;
      if (this.page < 1) this.page = 1;
      this.serchRequest();
    },
    jumpPagesNext() {
      this.page = this.getPage + this.showPageInPagination;
      if (this.page > this.getPageAmount)
        this.page = this.getPageAmount;
      this.serchRequest();
    },
    prevPage() {
        if (this.getPage > 1) {
          this.changePage(this.getPage - 1);
        }
      },
      nextPage() {
        const nextPage = this.getPage + 1;
        if (nextPage <= this.getPageAmount) {
          this.changePage(nextPage);
        }
      },
      scrollToTop() {
        
        window.scroll({
          top: this.getFilterTopPosition,
          left: 0,
          behavior: 'smooth'
        })
      },
      changePage(num) {
        if (this.getPage !== num) {
          this.page = num;
          this.serchRequest();
        }
      },
  },
};
