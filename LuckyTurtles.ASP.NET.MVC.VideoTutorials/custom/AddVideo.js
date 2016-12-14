$(function () {
    var TagContainer = $(".TagContainer");
    var errorMessage = $(".ErrMsg");
    var availableTags = [];

    //get all tags from DB with ajax
    $.get("/Video/GetTags", function (data) {
        for (var i = 0; i < data.length; i++) {        
            availableTags.push(data[i])
        }
    });


    //autocompelte based on DB 
    $("#Tag").autocomplete({
        source: availableTags
    });



    //adding tag  
    $("#AddTag").on("click", function()
    {
        //setting the primary things
        errorMessage.css("display", "none");
        var inputValue = $("#Tag").val();
        $("#Tag").val("");

        ////check valid input
        var validInput = checkValidInput(inputValue);
        if (validInput != true) {
            errorMessage.text("No custom tags allowed");
            errorMessage.css("display", "block");
            return;
        }

        //check if input is empty
        if (inputValue == "")
            return;

        //check the number of tags added to video
        var tagsCount = checkMaxCount();
        if (tagsCount == true)
            return;

        //check if tag already added
        var check = checkIfAdded(inputValue);
        if (check == true)
            return;

        //add tag to array
        AddToArray(inputValue);


        //append the tag to container div
        AppendToDiv();
       
    })

    function checkValidInput(inputValue) {
        for (var i = 0; i < availableTags.length; i++) {
            if (inputValue == availableTags[i]) {
                return true;
            }
        }
    }

    //remove clicked tag from list
    function removeTag() {
        var clicked = $(this).text();
        //find the tag that needs to be deleted
        for (var i = 0; i < tagArray.length; i++) {
            if (tagArray[i] == clicked) {
                tagArray.splice(i, 1);
                $("." + i + "").remove();
            };
        };

        //reset the added tags to make sure there is no brake in name numbers
        AppendToDiv();
    }

    //to make sure the user can not add more than 5 videos
    function checkMaxCount() {
        var Count = tagArray.length;
        if (Count >= 5) {
            errorMessage.text("You can add only 5 tags per video");
            errorMessage.css("display", "block");
            return true;
        }
    }

    //to make sure that user won't add one tag twice
    function checkIfAdded(inputValue) {
        var arrayLength = tagArray.length;
        for (var i = 0; i < arrayLength; i++) {
            if (tagArray[i] == inputValue) {
                errorMessage.text("Tag already added");
                errorMessage.css("display", "block");
                return true;
            }
        }
    }

    //add the tag to document
    function AppendToDiv() {
        //must reset every time to make sure that there is no brake in name numbers, we need the numbers to send the list to controler
        TagContainer.empty();
        for (var i = 0; i < tagArray.length; i++) {
            var TagToAppend = "<div class='addedTag " + i + "' name='Tag[" + i + "]'  id = '" + i + "'>" + tagArray[i] + "</div>";
            var hiddenInput = "<input class='hiddenInput " + i + "' name='Tag[" + i + "]' value='" + tagArray[i] + "'/>"
            TagContainer.append(TagToAppend);
            TagContainer.append(hiddenInput);
            var btn = $("#" + i + "");
            btn.click(removeTag);
        }
    }

    //add tag to array
    function AddToArray(inputValue) {
        tagArray.push(inputValue);
    }

    //array of tags added to the video
    var tagArray = [];
});
