Thank you for trying out AppsJS! Please contact Rodney at rbrooks33@gmail.com for questions/support/comments.

Make sure that you set the web server settings in AppsDeployments.json. For example:

  "Dev": {
    "Active": true,
    "Version": "1.0.1",
    "WebRoot": "http://127.0.0.1",
    "VirtualFolder": "",
    "Port": "44372",
    "AppsRoot": "",
    "Debug": false
  },

If you are using live-server, you can set your start folder at the index.html folder and, with the above configuration, run this to start out:

live-server --port=44372 --open=index.html