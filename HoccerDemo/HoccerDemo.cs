/*
Copyright (C) 2009, 2010, Hoccer GmbH Berlin, Germany <www.hoccer.com>

These coded instructions, statements, and computer programs contain
proprietary information of Linccer GmbH Berlin, and are copy protected
by law. They may be used, modified and redistributed under the terms
of GNU General Public License referenced below.

Alternative licensing without the obligations of the GPL is
available upon request.

GPL v3 Licensing:

This file is part of the "Linccer .Net-API".

Linccer .Net-API is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Linccer .Net-API is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Linccer .Net-API. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Threading;
using System.Web;
using LinccerApi;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace HoccerDemo
{
    class HoccerDemo
    {
        public static void Main (string[] args)
        {
            ClientConfig config = new ClientConfig ("C# Hoccer Demo");
            config.UseProductionServers (); // enables communication to real Hoccer Clients (iOS & Android)
            
            Linccer linccer = new Linccer ();
            linccer.Config = config;

            // set geo position (must be changed to work on other locations than Molkenmarkt 2 in Berlin, Germany)
			//41.38690, 2.16532
			linccer.Gps = new LocationInfo { Latitude = 41.38690, Longitude = 2.16532, Accuracy = 1000 };
            linccer.SubmitEnvironment ();

            // inialize filecache for temporary up- and downloading large files (not used jet)
            FileCache cache = new FileCache ();
            cache.Config = config;

            // countdown to not miss the time when the drag-in or -out gesture must take place on the mobile
            for (int i = 3; i > 0; i--) {
                System.Console.Write (i + "... ");
                Thread.Sleep (1 * 1000);
            }
            
            if (args.Length > 0) {

                // create a plain message
                Hoc hoc = new Hoc ();
                hoc.DataList.Add(
                  new HocData {Content = "yaya", Type = "text/plain"}
                );

                // share it 1:1, in the Hoccer mobile App, you need to perform a drag in
                // gesture to receive the message  (one-to-many is throw/catch)
                linccer.Share ("one-to-one", hoc);

            } else {
                System.Console.WriteLine ("Waiting for sender");
                Hoc hoc;

                // receive 1:1, in the Hoccer mobile App, you need to perform a drag out
                // gesture to send something to this client (one-to-many is throw/catch)
                hoc = linccer.Receive<Hoc> ("one-to-one");
                
                if (hoc == null)
                    System.Console.WriteLine ("no sender found");
                else
                    System.Console.WriteLine (hoc);
            }
        }
    }
}
