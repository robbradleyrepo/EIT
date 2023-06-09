export const API = {
    downloadDocument(url, data, title = 'document', extension = '', type = 'POST' ) {
        document.body.style.cursor = "wait";
        $.ajax({
          type,
          xhrFields: { responseType: "arraybuffer" },
          url,
          data,
        })
          .done((data, status, xhr) => {
            var documentType = xhr.getResponseHeader("document-type");
            const url = window.URL.createObjectURL(
              new Blob([data], { type: documentType+";charset=base-64" })
            );
            const link = document.createElement("a");
            link.href = url;
            link.setAttribute("download", title + extension);
            document.body.appendChild(link);
            link.click();
            document.body.style.cursor = "default";
          })
          .fail((e) => {
            console.error(e);
            document.body.style.cursor = "default";
          });
      }
}
