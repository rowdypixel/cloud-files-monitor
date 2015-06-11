Cloud Files Monitor
===
Cloud Files Monitor is an extensible application which keeps an eye on the files in your CDN and notifies you if they change.

Settings
===
Configuration of the monitor is handled via `App.config`. Inside the `appSettings` node, there are a number of keys to change various parts of the system.

1. `FileTrackerDatabasePath` - This sets the path of the SQLite database which stores information about the files in your sites. Default: `.\files.db`
2.  `SitesConfigFile` - This sets the path of the JSON file which stores information about how which sites to be monitored and how to access them. Default: `.\sites.json`
3.  `TimerCheckIntervalSeconds` - This sets the number of seconds between each check of your sites. Default: `30`
4. `NotificationEmailAddress` - This sets the email address where notifications will be sent. Default: `test@example.com`
5. `NotificationSenderAddress` - This sets the sender email address for notification emails. Default: `noreply@example.com`
6.  `ServerHost` - This sets the host for the server which lets your mark changes as accepted. Default: `localhost`
7. `ServerPort` - This sets the port for the server which lets your mark changes as accepted. Default: `1485`
8. `SendgridAPIUser` - This is the SendGrid username that is used to send notification emails. Default: `test`
9. `SendgridAPIKey` - This is the SendGrid password that is used to send notification emails. Default: `test`


Sites JSON file
===
An example JSON file can be found below:

	[
	  {
	    "Name": "Your Site",
	    "ContainerName": "YourSiteContainer",
      "RestoreCommand" : "C:\Sites\DeploySite.bat",
	    "Provider": {
	      "$type": "CloudFilesMonitor.CloudProviders.RackspaceCloudProvider, CloudFilesMonitor",
	      "FriendlyName": "Rackspace Cloud Files",
	      "CodeName": "RackspaceCloudFiles",
	      "AuthDetails": {
	        "APIKey": "your_rackspace_api_key",
	        "Username": "your_rackspace_username"
	      }
	    }
	  }
	]


Usage
===

Configure the application with the settings listed above, then run `CloudFilesMonitor.exe`

License
===
Cloud Files Monitor is released under the terms of the MIT license.
