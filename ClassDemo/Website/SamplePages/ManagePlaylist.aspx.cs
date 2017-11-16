using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using Chinook.Data.POCOs;

#endregion
public partial class SamplePages_ManagePlaylist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            TracksSelectionList.DataSource = null;
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        //PreRenderComplete occurs just after databinding page events
        //load a pointer to point to your DataPager control
        DataPager thePager = TracksSelectionList.FindControl("DataPager1") as DataPager;
        if (thePager !=null)
        {
            //this code will check the StartRowIndex to see if it is greater that the
            //total count of the collection
            if (thePager.StartRowIndex > thePager.TotalRowCount)
            {
                thePager.SetPageProperties(0, thePager.MaximumRows, true);
            }
        }
    }

    protected void ArtistFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        TracksBy.Text = "Artist";
        SearchArgID.Text = ArtistDDL.SelectedValue;
        //refresh the track list display
        TracksSelectionList.DataBind();
    }

    protected void MediaTypeFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        TracksBy.Text = "MediaType";
        SearchArgID.Text = MediaTypeDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void GenreFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        TracksBy.Text = "Genre";
        SearchArgID.Text = GenreDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void AlbumFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        TracksBy.Text = "Album";
        SearchArgID.Text = ArtistDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void PlayListFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        //standard query lookup
        if (String.IsNullOrEmpty(PlaylistName.Text))
        {
            //able to display a message to the user via the MessageUserControl
            //one of the methods of MessageUserControl is .ShowInfo()
            MessageUserControl.ShowInfo("Warning", "PlayList NAme is Required.");
        }
        else
        {
            //obtain the username from the security Identity Class
            string username = User.Identity.Name;

            //the MessageUserControl has embedded in its code the try/catch logic
            //you do not need to code your own try catch
            MessageUserControl.TryRun(() =>
            {
                //Code to be run under the watchful eyes of the user control
                //this is the try{your code} of the try/catch
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                PlayList.DataSource = info;
                PlayList.DataBind();
            },"this is the title","here is your current playlist");
        }
    }

    protected void TracksSelectionList_ItemCommand(object sender, 
        ListViewCommandEventArgs e)
    {
        //code to go here
        if (String.IsNullOrEmpty(PlaylistName.Text))
        {
            MessageUserControl.ShowInfo("Warning", "PlayList Name is Required.");
        }
        else
        {
            string username = User.Identity.Name;

            //where does trackid come from
            // ListViewCommandEventArgs e contains the parameter values for this event;
            // CommandArgument
            //CommandArgument is an object
            int trackid = int.Parse(e.CommandArgument.ToString());

            //send you collection of parameter values to the BLL for processing
            MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> refreshResults = sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, trackid);
                    PlayList.DataSource = refreshResults;
                    PlayList.DataBind();
            },"Success","Your track has been added to your Playlist");

        }

    }

    protected void MoveUp_Click(object sender, EventArgs e)
    {
        //code to go here

        //is there a list
        if (PlayList.Rows.Count == 0)
        {
            MessageUserControl.ShowInfo("Warning", "No Playlist has been retrieved.");
        }
        else
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Warning", "No Play List Name has been entered.");
            }
            else
            {
                //check only one row has been selected
                int trackid = 0;
                int tracknumber = 0;
                int rowsSelected = 0;   //counter
                CheckBox playlistselection = null;
                //traverse the gridview checking each row for a checked box
                for (int i = 0; i < PlayList.Rows.Count; i++)
                {
                    //playlistselection will point to the current checkbox of the current gridview row being examined
                    playlistselection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                    if (playlistselection.Checked)
                    {
                        trackid = int.Parse((PlayList.Rows[i].FindControl("Trackid") as Label).Text);
                        tracknumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        rowsSelected++;
                    }
                }//eo for
                if (rowsSelected != 1)
                {
                    MessageUserControl.ShowInfo("Warning", "Select only one track.");
                }
                else
                {
                    //is this the top Track
                    if (tracknumber == 1)
                    {
                        MessageUserControl.ShowInfo("Information", "Track cannot be moved up");
                    }
                    else
                    {
                        
                        MoveTrack(trackid, tracknumber, "up");
                    }
                }
            }
        }
    }

    protected void MoveDown_Click(object sender, EventArgs e)
    {
        //code to go here

        //is there a list
        if (PlayList.Rows.Count == 0)
        {
            MessageUserControl.ShowInfo("Warning", "No Playlist has been retrieved.");
        }
        else
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Warning", "No Play List Name has been entered.");
            }
            else
            {
                //check only one row has been selected
                int trackid = 0;
                int tracknumber = 0;
                int rowsSelected = 0;   //counter
                CheckBox playlistselection = null;
                //traverse the gridview checking each row for a checked box
                for (int i = 0; i < PlayList.Rows.Count; i++)
                {
                    //playlistselection will point to the current checkbox of the current gridview row being examined
                    playlistselection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                    if (playlistselection.Checked)
                    {
                        trackid = int.Parse((PlayList.Rows[i].FindControl("Trackid") as Label).Text);
                        tracknumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        rowsSelected++;
                    }
                }//eo for
                if (rowsSelected != 1)
                {
                    MessageUserControl.ShowInfo("Warning", "Select only one track.");
                }
                else
                {
                    //is this the bottom Track
                    if (tracknumber == PlayList.Rows.Count)
                    {
                        MessageUserControl.ShowInfo("Information", "Track cannot be moved down");
                    }
                    else
                    {
                        MoveTrack(trackid, tracknumber, "down");
                    }
                }
            }
        }
    }
    protected void MoveTrack(int trackid, int tracknumber, string direction)
    {
        //code to go here
        //Wrap up your work under MessageUserControl
        MessageUserControl.TryRun(() =>
        {
            //standard update call to your BLL
            PlaylistTracksController sysmgr = new PlaylistTracksController();
            //call the appropriate BLL method (update)
            sysmgr.MoveTrack(User.Identity.Name, PlaylistName.Text,trackid,tracknumber, direction);

            //refresh the display
            List<UserPlaylistTrack> results = sysmgr.List_TracksForPlaylist(PlaylistName.Text, User.Identity.Name);
            PlayList.DataSource = results;
            PlayList.DataBind();
        },"Success","Track has been moved");
        
    }
    protected void DeleteTrack_Click(object sender, EventArgs e)
    {
        //code to go here
    }
}
