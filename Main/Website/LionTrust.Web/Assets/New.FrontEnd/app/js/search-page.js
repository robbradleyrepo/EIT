document.addEventListener("DOMContentLoaded", () => {
    const host = 'http://localhost:3004/results'
    var app = new Vue({
        el: '#result-list-app',
        data: {
          results: []
        },
        methods: {
            getSearchItems() {
                return fetch(`${host}`)
            }
        },
        mounted() {
            fetch(host)
                .then(response => response.json())
                .then(json => this.results = json)
            console.log(this.results);
        }
      })
  });