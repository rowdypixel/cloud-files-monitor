﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CloudFilesMonitor
{
    class Site
    {
        public string Name { get; set; }
        public string ContainerName { get; set; }

        /// <summary>
        /// Compares the files on the server to the existing records in the database.
        /// </summary>
        /// <returns>A set of which files are different on the server.</returns>
        public MD5Result[] Compare()
        {
            var existingQuery = string.Format("SELECT * FROM cfm_files WHERE SiteName = '{0}'", this.Name);
            var existing = Database.Helper.CurrentHelper.GetDataTable(existingQuery);

            var filesOnServer = Provider.GetFiles(ContainerName);

            if (existing.Rows.Count == 0)
            {
                SetCurrentAsValid(filesOnServer);
                return new MD5Result[0];
            }


            List<MD5Result> changes = new List<MD5Result>();
            foreach (var file in filesOnServer) 
            { 
                var queryableResults = existing.AsEnumerable();
                var hasChanged = queryableResults.Where(x =>
                    x.Field<string>("CloudPath") == file.CloudPath
                    && x.Field<string>("MD5Hash") == file.MD5
                    ).Count() != 1;

                if (hasChanged)
                    changes.Add(file);
            }

            return changes.ToArray();
        }

        public void SetCurrentAsValid()
        {
            var filesOnServer = Provider.GetFiles(ContainerName);
            SetCurrentAsValid(filesOnServer);
        }
        public void SetCurrentAsValid(IEnumerable<MD5Result> files)
        {
            // Delete any existing files.
            Database.Helper.CurrentHelper.Delete("cfm_files", string.Format("SiteName = '{0}'", this.Name));

            // Add the files back to the DB.
            foreach (var file in files)
            {
                Dictionary<string, string> columns = new Dictionary<string, string>()
                    {
                        {"SiteName", this.Name},
                        {"CloudPath", file.CloudPath},
                        {"MD5Hash", file.MD5}
                    };
                Database.Helper.CurrentHelper.Insert("cfm_files", columns);
            }

        }

        public ICloudProvider Provider { get; set; }
    }
}
