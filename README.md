# Metro

This Application is designed to show route information about the LA Metro Api.

# Prerequistates

This application uses .Net 4.5.2. Available [here.] (https://www.microsoft.com/en-us/download/details.aspx?id=42643)

You will need Visual Studio to open the solution Available [here.] (https://www.microsoftstore.com/store/msusa/en_US/cat/Visual-Studio-2015/categoryID.69407500?WT.mc_id=pointitsem+Google+Adwords+5+-+Visual+Studio+2015&s_kwcid=AL!4249!3!140972907118!e!!g!!visual%20studio%202015&invsrc=search&ef_id=V7dsEQAAADEGR7du:20161211183832:s)

External Libraries are obtained from nuget, avaiable [here.] (https://www.nuget.org/) (Also available in the visual studio extensions gallery)  Libraries are referenced in the packages.config files.

Visual studio allows you to run this project in debug mode without setting up an IIS site. For a more persistent server application, set up an IIS site with the build artifacts from this code.


# Notes

Though the example API page says to use "http://api.metro.net/agencies/lametro/routes/704" to retrieve alls stops, you must use the stops  "http://api.metro.net/agencies/lametro/routes/704/stops/" route.
