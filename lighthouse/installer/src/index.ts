import axios from 'axios'
import ApiClient from '@lhci/utils/src/api-client.js'

const appName = process.argv[2];
const username = process.argv[3];
const password = process.argv[4];
const bitbucketUsername = process.argv[5];
const bitbucketPassword = process.argv[6];
const api = new ApiClient({    
    basicAuth: {
      username:username,
      password:password
    },
    rootURL: `https://${appName}.herokuapp.com/`,
  });
api.createProject({
  name: "lighthouse",
  externalUrl: "http://github.com",
  baseBranch: "master",
  slug: '', // this property is dynamically generated server-side
}).then((project: { token: string; }) => {
    axios.put('https://api.bitbucket.org/2.0/repositories/sagittarius-marketing/liontrustfrontend/pipelines_config/variables/{d051a500-9585-478c-98d1-8071c20f0ec7}',
    {
      "value": project.token
    }, 
    {
      auth:{
        username:bitbucketUsername,
        password:bitbucketPassword
      }
    });
});
