using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {

                //what would happen if there is no match for the
                // incoming parameter values
                //we need to ensure that the results have a valid value
                //this value will be the result of a query either a null (not found)
                // of an IEnumerable<T> collection
                //to achieve a valid value encapsulate the query in a 
                //  .FirstOFDefault()
                var results = (from x in context.Playlists
                              where x.UserName.Equals(username)
                              && x.Name.Equals(playlistname)
                              select x).FirstOrDefault();

               //test if you should return a null as your collection
               //   or find the tracks for the given PlaylistId in the results
                if (results == null)
                {
                    return null;
                }
                else
                {
                //now get the tracks
                var theTracks = from x in context.PlaylistTracks
                                where x.Playlist.Equals(results.PlaylistId)
                                orderby x.TrackNumber
                                select new UserPlaylistTrack
                                {
                                    TrackID = x.TrackId,
                                    TrackNumber = x.TrackNumber,
                                    TrackName = x.Track.Name,
                                    Milliseconds = x.Track.Milliseconds,
                                    UnitPrice = x.Track.UnitPrice
                                };
                
                return theTracks.ToList();
                }
            }
        }//eom
        public List<UserPlaylistTrack> Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //code to go here
                //Part One:
                //query to get the Playlist id
                var exists = (from x in context.Playlists
                               where x.UserName.Equals(username)
                               && x.Name.Equals(playlistname)
                               select x).FirstOrDefault();
                //intialize the tracknumber
                int tracknumber = 0;
                //I will need to create an instance of PlayListTrack
                PlaylistTrack newTrack = null;

                //determine if a playlist "parent" instance needs to be created
                if (exists == null)
                {
                    //this is a new PlayList
                    //create an instance of playlist to add to PlayList table
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    exists = context.Playlists.Add(exists);
                    //at this time there is NO physical PKey
                    //the psuedo PKey is handled by the HashSet
                    tracknumber = 1;
                }
                else
                {
                    //playlist exists
                    //I need to generate the next TrackNumber
                    tracknumber = exists.PlaylistTracks.Count() + 1;

                    //validation: om our example a track can only exist once in a particular playlist
                    newTrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    if (newTrack != null)
                    {
                        throw new Exception("Playlist already has requested track");
                    }
                }

                //Part Two: Add the PlayListTrack instance
                //use naviagtion to .Add the new track to PlaylistTrack
                newTrack = new PlaylistTrack();
                newTrack.TrackId = trackid;
                newTrack.TrackNumber = tracknumber;

                //NOTE: the pkey for the PlaylistId may not yet exist
                //  USING navigation one can let HashSet handle the PlaylistId
                // pkey value
                exists.PlaylistTracks.Add(newTrack);

                //physically add all data to the database
                context.SaveChanges();
                return List_TracksForPlaylist(playlistname, username);
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
