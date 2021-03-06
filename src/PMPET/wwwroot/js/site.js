﻿// Write your Javascript code.
function displayHelpInfo(whichOne) {

    var currentStatus = document.getElementById("infoPanel").getAttribute("style");
    if (currentStatus == "visibility: hidden") {
        switch (whichOne) {
            case 'Property':
                document.getElementById("infoPanel").setAttribute("style", "visibility: visible");
                document.getElementById("infoPanelContainer").setAttribute("style", "border:1px;background-color:#ffe6e6;color:black;border-radius:5px;");
                document.getElementById("infoPanel").textContent = "Any property that the estate agent is marketing must be recorded through this section. While being able to view the existing particulars of ";
                document.getElementById("infoPanel").textContent += "currently marketed properties, users are also able to add a new property or delete an existing one.  Users can also re-arrange the list by clicking the";
                document.getElementById("infoPanel").textContent += "heading of each sort-enabled information.  Please note that these column headings are in color for ease of reference.  Details section will display the full information";
                document.getElementById("infoPanel").textContent += "about any particular property including any viewing which may have been arranged.";
                break;
            case 'Members':
                document.getElementById("infoPanel").setAttribute("style", "visibility: visible");
                document.getElementById("infoPanelContainer").setAttribute("style", "border:1px;background-color:#ffe6e6;color:black;border-radius:5px;");
                document.getElementById("infoPanel").textContent = "Individuals -vendors, buyers and viewers- who has dealings with the estate agent are all treated as Members. Pickme Properties";
                document.getElementById("infoPanel").textContent += "doesn't restrict any member to be of a certain type thus a member can be a buyer, vendor or just a viewer. While being able to view the details of";
                document.getElementById("infoPanel").textContent +="existing members, users are also able to add a new member or delete an existing one.  Users can also re-arrange the list of members by clicking the";
                document.getElementById("infoPanel").textContent +="heading of each column.  Please note that these column headings are in colour for ease of reference.  Details section will display the full information";
                document.getElementById("infoPanel").textContent +="about any particular member including any property or viewing they may have been linked to.";
                break;
            case 'Viewings':
                document.getElementById("infoPanel").setAttribute("style", "visibility: visible");
                document.getElementById("infoPanelContainer").setAttribute("style", "border:1px;background-color:#ffe6e6;color:black;border-radius:5px;");
                document.getElementById("infoPanel").textContent = "All members are allowed to arrange a viewing and details are recorded and monitored in this section.";
                document.getElementById("infoPanel").textContent +="While being able to browse through the existing particulars of arranged viewings, users are also able to add a new viewing or delete an existing one.";
                document.getElementById("infoPanel").textContent += "Users can also re-arrange the list of viewings by clicking the heading of each sort-enabled information.  Please note that these column headings are";
                document.getElementById("infoPanel").textContent +="in colour for ease of reference.  Details section will display the full information about any particular viewing including notes that may have been added.";
                break;
            case 'Services':
                document.getElementById("infoPanel").setAttribute("style", "visibility: visible");
                document.getElementById("infoPanelContainer").setAttribute("style", "border:1px;background-color:#ffe6e6;color:black;border-radius:5px;");
                document.getElementById("infoPanel").textContent = "Pickme Properties offers several services regarding the real estates they have in their list. Although these can mainly be classified as Property Sale";
                document.getElementById("infoPanel").textContent +="and Property Rental services, Pickme Properties has varieties of these services based on fees charged.  Users are therefore able to define a service";
                document.getElementById("infoPanel").textContent +="and its financial details as well as service start and end dates. While being able to browse through the existing particulars of existing services,";
                document.getElementById("infoPanel").textContent +="users are also able to add a new service or delete an existing one.  Please note that users are not able to re-arrange the list of services.Details section";
                document.getElementById("infoPanel").textContent +="will display the full information about any particular service including start and end dates of service.";
                break;
            case 'Offers':
                document.getElementById("infoPanel").setAttribute("style", "visibility: visible");
                document.getElementById("infoPanelContainer").setAttribute("style", "border:1px;background-color:#ffe6e6;color:black;border-radius:5px;");
                document.getElementById("infoPanel").textContent = "Pickme Properties needs to keep a track of offers received  (either sale or rental) for a particular property. While being able to browse through details";
                document.getElementById("infoPanel").textContent +="of existing offers, users are also able to add a new offer or delete an existing one.  Please note that users are not able to re-arrange the list of offers.";
                document.getElementById("infoPanel").textContent += "Details section will display the full information about any particular offer including any notes which may have been entered regarding the offer.";
                break;
            default:
                document.getElementById("infoPanel").setAttribute("style", "visibility: visible");
                document.getElementById("infoPanelContainer").setAttribute("style", "border:1px;background-color:#ffe6e6;color:black;border-radius:5px;");
                document.getElementById("infoPanel").textContent = "Unknown type information request";
        }

    } else {
        document.getElementById("infoPanel").setAttribute("style", "visibility: hidden");
        document.getElementById("infoPanelContainer").setAttribute("style", "");
    }


}