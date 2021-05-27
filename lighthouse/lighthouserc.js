const url = `https://${process.env.LIGHTHOUSE_HEROKU_APP_NAME}.herokuapp.com/`
module.exports = {
    ci: {
        collect: {
            url: ['https://cm-liontrust-it.sagittarius.agency/', 
            'https://cm-liontrust-it.sagittarius.agency/articles/improving-prospects-for-the-land-of-the-rising-sun'],   
            settings:{
                chromeFlags:'--no-sandbox'
            }   
        },
        upload: {
            target: 'lhci',
            serverBaseUrl: url,
            token: process.env.LIGHTHOUSE_KEY, // could also use LHCI_TOKEN variable instead
          },
    },
  };