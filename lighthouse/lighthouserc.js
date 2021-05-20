const url = `https://${process.env.LIGHTHOUSE_HEROKU_APP_NAME}.herokuapp.com/`
module.exports = {
    ci: {
        collect: {
            url: ['http://www.liontrust.co.uk/'],   
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