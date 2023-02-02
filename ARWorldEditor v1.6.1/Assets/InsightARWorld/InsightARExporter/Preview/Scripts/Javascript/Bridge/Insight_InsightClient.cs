using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight
{
    public class InsightClient
    {
        private string clientAddress;
        private int clientPort;
        private bool usingSSL;

        public InsightClient(string address,int port, bool using_ssl) {

            clientAddress = address;
            clientPort = port;
            usingSSL = using_ssl;
        }

        public int status {
            get;
        }

        public void disconnect()
        {

        }

        public void send( string msg, int size )
        {

        }

        public int tryRecv()
        {

            return 0;
        }

        public int sendInitUserContextRequest(
            string gameServerId,
            string secret,
            string divisionId,
            string divisionIdSpace,
            int roomNumber,
            int position,
            int keepUserSecondsOnNetworkError)
        {

            return 0;
        }


        public int sendRestoreUserContextRequest(
            string gameServerId,
            string token )
        {

            return 0;
        }

        public int sendClientHeartbeatRequest( string random )
        {

            return 0;
        }

    }

}


