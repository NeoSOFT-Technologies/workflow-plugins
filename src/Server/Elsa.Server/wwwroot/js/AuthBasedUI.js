$(document).ready(function () {

    //if (localStorage.scopes == null)
    //    window.location = "/login";

    //localStorage.accessToken = "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJtTjJ1NE1hM2Qtc1BRcDBzYnZTUVp1UXpaT19jWDVTSHFuMTE3U1RaRF9jIn0.eyJleHAiOjE2NTQ4OTQyNjYsImlhdCI6MTY1NDg1ODI2NiwianRpIjoiZjE4YWZjMTctNTg4MC00NjRiLThkNTMtMGJlZDM1M2IzYzYyIiwiaXNzIjoiaHR0cHM6Ly9pYW0ta2V5Y2xvYWsubmVvc29mdHRlY2guY29tL2F1dGgvcmVhbG1zL21hc3RlciIsImF1ZCI6WyJUZW5hbnQxLXJlYWxtIiwibWFzdGVyLXJlYWxtIiwiYWNjb3VudCJdLCJzdWIiOiIzODM2ODc2YS0xMjVhLTQ5M2ItYjQzZC03YzAxMzE4ODVlMmIiLCJ0eXAiOiJCZWFyZXIiLCJhenAiOiJhZG1pbi1jbGkiLCJzZXNzaW9uX3N0YXRlIjoiMzcwMjJkMWMtZTFjMC00YWFiLWE5YzgtZWIyZGMyODVmNGY4IiwiYWNyIjoiMSIsInJlYWxtX2FjY2VzcyI6eyJyb2xlcyI6WyJjcmVhdGUtcmVhbG0iLCJkZWZhdWx0LXJvbGVzLW1hc3RlciIsIm9mZmxpbmVfYWNjZXNzIiwiYWRtaW4iLCJ1bWFfYXV0aG9yaXphdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7IlRlbmFudDEtcmVhbG0iOnsicm9sZXMiOlsidmlldy1pZGVudGl0eS1wcm92aWRlcnMiLCJ2aWV3LXJlYWxtIiwibWFuYWdlLWlkZW50aXR5LXByb3ZpZGVycyIsImltcGVyc29uYXRpb24iLCJjcmVhdGUtY2xpZW50IiwibWFuYWdlLXVzZXJzIiwicXVlcnktcmVhbG1zIiwidmlldy1hdXRob3JpemF0aW9uIiwicXVlcnktY2xpZW50cyIsInF1ZXJ5LXVzZXJzIiwibWFuYWdlLWV2ZW50cyIsIm1hbmFnZS1yZWFsbSIsInZpZXctZXZlbnRzIiwidmlldy11c2VycyIsInZpZXctY2xpZW50cyIsIm1hbmFnZS1hdXRob3JpemF0aW9uIiwibWFuYWdlLWNsaWVudHMiLCJxdWVyeS1ncm91cHMiXX0sIm1hc3Rlci1yZWFsbSI6eyJyb2xlcyI6WyJ2aWV3LWlkZW50aXR5LXByb3ZpZGVycyIsInZpZXctcmVhbG0iLCJtYW5hZ2UtaWRlbnRpdHktcHJvdmlkZXJzIiwiaW1wZXJzb25hdGlvbiIsImNyZWF0ZS1jbGllbnQiLCJtYW5hZ2UtdXNlcnMiLCJxdWVyeS1yZWFsbXMiLCJ2aWV3LWF1dGhvcml6YXRpb24iLCJxdWVyeS1jbGllbnRzIiwicXVlcnktdXNlcnMiLCJtYW5hZ2UtZXZlbnRzIiwibWFuYWdlLXJlYWxtIiwidmlldy1ldmVudHMiLCJ2aWV3LXVzZXJzIiwidmlldy1jbGllbnRzIiwibWFuYWdlLWF1dGhvcml6YXRpb24iLCJtYW5hZ2UtY2xpZW50cyIsInF1ZXJ5LWdyb3VwcyJdfSwiYWNjb3VudCI6eyJyb2xlcyI6WyJtYW5hZ2UtYWNjb3VudCIsIm1hbmFnZS1hY2NvdW50LWxpbmtzIiwidmlldy1wcm9maWxlIl19fSwic2NvcGUiOiJwcm9maWxlIGVtYWlsIiwic2lkIjoiMzcwMjJkMWMtZTFjMC00YWFiLWE5YzgtZWIyZGMyODVmNGY4IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJuYW1lIjoiU2FudG9zaCBTaGluZGUiLCJwZXJtaXNzaW9uIjpbImNyZWF0ZSIsInZpZXciLCJlZGl0IiwiZGVsZXRlIl0sInByZWZlcnJlZF91c2VybmFtZSI6ImFkbWluIiwiZ2l2ZW5fbmFtZSI6IlNhbnRvc2giLCJmYW1pbHlfbmFtZSI6IlNoaW5kZSIsImVtYWlsIjoic2FudG9zaC5zaGluZGVAbmVvc29mdHRlY2guY29tIn0.I2Oc4qcGIKt9KoYCq-CNSM5Tr294Sb2_C8xAfliePtQCGt2tjGNGLKFeQI39cZWORNNgni-y1kouCjLe4tER6dgq2QHSV-5VRiiaXmEX0Z5ktGrRAsFF_G1O8QYlqgSxESPqE_2MHHV5fI7BsgTMcjRtQgV2yKtQi6hycxum33l0slrQoHwx6wO4oc38-iqcPovNrm0Vy1au-PtF9TGCK8uwDtliN5-7W8aLLRqCZSG2ykGOKZUJVaXxJLOClhhrN3l-tNH7FJU5RXsJaYiaToiBbptDqPqbxQwR-C7Y2LZbR2k5_ieCEvAOZryI2fUitgz260NFZtAg73Pm0S-Ulg";

    if (localStorage.accessToken == null) {
        window.location = "/login";
    }
    else {
        var token = localStorage.accessToken;
        var jwtData = token.split('.')[1]
        var decodedJwtJsonData = window.atob(jwtData)
        var decodedJwtData = JSON.parse(decodedJwtJsonData)
        console.log(decodedJwtData);
        console.log(decodedJwtData.permission);
        if (decodedJwtData.permission == null)
            window.location = "/login";
        //else
        //    localStorage.permission = JSON.stringify(decodedJwtData.permission);
    }

    var wfDefinitionPerm = decodedJwtData.permission;
    var wfInstancePerm = decodedJwtData.permission;


    console.log(wfDefinitionPerm.length);
    console.log(wfInstancePerm.length);
    //var tmp = JSON.parse(localStorage.scopes);
    //tmp.forEach((obj) => {
    //    if (obj.rsName == 'workflow-definitions')
    //        wfDefinitionPerm = obj.scopes;

    //    if (obj.rsName == 'workflow-instances')
    //        wfInstancePerm = obj.scopes;
    //});

    MutationObserver = window.MutationObserver || window.WebKitMutationObserver;

    var observer = new MutationObserver(function (mutations, observer) {

        //alert("mutation observer called");

        //condition checks workflow definition page
        if (window.location.href.indexOf("/workflow-definitions") > -1) {
            //condition for view only permission
            if (wfDefinitionPerm.length == 1 && wfDefinitionPerm[0] == "view") {
                if (window.location.pathname == "/workflow-definitions") // if for all workflow list page
                {
                    rmCreateBtn();
                }
                else    // else for workflow editor page
                {
                    viewOnlyWorkflow();
                }
            }

            //for any 2 permissions in scopes list
            else if (wfDefinitionPerm.length == 2) {
                //for view & create permssion 
                if (jQuery.inArray("create", wfDefinitionPerm) != -1) {
                    if (window.location.pathname == "/workflow-definitions") // if for all workflow list page
                    {
                        //Remove delete workflow element from option buttons
                        $("elsa-context-menu a:contains('Delete')").parent().remove();

                        //Remove edit workflow element from option buttons
                        $("elsa-context-menu a:contains('Edit')").parent().remove();
                    }
                    else // else for workflow editor page
                    {
                        viewOnlyWorkflow();

                    }
                }

                //condition for view, edit permission
                if (jQuery.inArray("edit", wfDefinitionPerm) != -1) {
                    if (window.location.pathname == "/workflow-definitions") // if for all workflow list page
                    {
                        //Remove delete workflow element from option buttons
                        $("elsa-context-menu a:contains('Delete')").parent().remove();

                        //Remove create workflow button
                        $("a[href='/workflow-definitions/new']").parent().remove();

                    }
                    else // else for workflow editor page
                    {
                        //No action required
                    }
                }

                //condition for view, delete permission
                if (jQuery.inArray("delete", wfDefinitionPerm) != -1) {
                    if (window.location.pathname == "/workflow-definitions") // if for all workflow list page
                    {
                        //Remove create workflow button
                        $("a[href='/workflow-definitions/new']").parent().remove();

                        //Remove edit workflow element from option buttons
                        $("elsa-context-menu a:contains('Edit')").parent().remove();

                        //Remove publish workflow element from option buttons
                        $("elsa-context-menu a:contains('Publish')").parent().remove();
                    }
                    else    // else for workflow editor page
                    {
                        //No action required
                    }
                }

            }

            //for any 3 permissions in scopes list
            else if (wfDefinitionPerm.length == 3) {
                //for view, create & edit permssion 
                if (jQuery.inArray("delete", wfDefinitionPerm) == -1) {
                    if (window.location.pathname == "/workflow-definitions") // if for all workflow list page
                    {
                        //Remove delete workflow element from option buttons
                        $("elsa-context-menu a:contains('Delete')").parent().remove();
                    }
                    else    // else for workflow editor page
                    {
                        //No action required
                    }
                }

                //for view, edit & delete permssion 
                if (jQuery.inArray("create", wfDefinitionPerm) == -1) {
                    if (window.location.pathname == "/workflow-definitions") // if for all workflow list page
                    {
                        //Remove create workflow button
                        $("a[href='/workflow-definitions/new']").parent().remove();
                    }
                    else    // else for workflow editor page
                    {
                        //No action required
                    }
                }

                //for view, create & delete permssion 
                if (jQuery.inArray("edit", wfDefinitionPerm) == -1) {
                    if (window.location.pathname == "/workflow-definitions") // if for all workflow list page
                    {
                        //Remove edit workflow element from option buttons
                        $("elsa-context-menu a:contains('Edit')").parent().remove();
                    }
                    else    // else for workflow editor page
                    {
                        viewOnlyWorkflow();
                    }
                }

            }
            else if (wfDefinitionPerm.length == 4) {
                //Do nothing
            }

            else {
                alert("Error in permission! contact dev team");
                window.location = "/";
            }

        }

        if (window.location.pathname == "/workflow-instances") {
            //condition for view only permission
            if (wfInstancePerm.length == 1 && wfInstancePerm[0] == "view") {
                //Remove delete workflow instance element from option buttons
                $("elsa-dropdown-button button:contains('Bulk Actions')").parent().parent().remove();

                //Remove options button from instances
                $("elsa-context-menu").remove();
            }
            else if (wfInstancePerm.length == 2) {
                //for view & delete permission
                if (jQuery.inArray("delete", wfInstancePerm) != -1) {
                    //Remove View and Cancel workflow instance element from option buttons
                    $("elsa-context-menu a:contains('View')").parent().remove();
                    $("elsa-context-menu a:contains('Cancel')").parent().remove();

                    //Remove cancel and retry buttons from bulk action dropdown
                    $("elsa-dropdown-button a:contains('Cancel')").remove();
                    $("elsa-dropdown-button a:contains('Retry')").remove();

                }
            }

            else if (wfInstancePerm.length == 3) {
                //for view, create & edit permission 
                if (jQuery.inArray("delete", wfDefinitionPerm) == -1) {
                    //Remove Delete workflow instance element from option buttons
                    $("elsa-context-menu a:contains('Delete')").parent().remove();

                    //Remove Delete buttons from bulk action dropdown
                    $("elsa-dropdown-button a:contains('Delete')").remove();
                }
            }

            else if (wfInstancePerm.length == 4) {
                //Do nothing
            }
            else {
                alert("Error in permission! contact dev team");
                window.location = '/';
            }

        }


        if (window.location.pathname == "/workflow-registry") {
            if (wfDefinitionPerm.length == 1 && wfDefinitionPerm[0] == "view") {
                //remove create workflow button
                rmCreateBtn();
            }

            else if (wfDefinitionPerm.length == 2) {
                //condition for view, edit permission
                if (jQuery.inArray("edit", wfDefinitionPerm) != -1) {
                    //Remove create workflow button
                    $("a[href='/workflow-definitions/new']").parent().remove();
                }
            }

            else if (wfDefinitionPerm.length == 3) {
                if (jQuery.inArray("create", wfDefinitionPerm) == -1)
                    $("a[href='/workflow-definitions/new']").parent().remove(); //Remove create workflow button 

            }
            else if (wfDefinitionPerm.length == 4) {
                //do nothing
            }
            else {
                alert("Error in permission! contact dev team");
                window.location = '/';
            }
        }

    });

    // define what element should be observed by the observer
    // and what types of mutations trigger the callback
    observer.observe(document, {
        subtree: true,
        attributes: true
    });

});

//Func to make workflow editor page viewonly
const viewOnlyWorkflow = () => {

    setTimeout(() => {
        $("elsa-workflow-definition-editor-screen input").prop('disabled', true);
        $("elsa-workflow-definition-editor-screen textarea").prop('disabled', true);
        $("elsa-workflow-definition-editor-screen button").prop('disabled', true);
        $("elsa-workflow-publish-button").parent().parent().remove();
        $("div .elsa--mt-2").remove();
        $('.add *').click(false);
        $('button[type=submit]').remove();
        $('.modal-content button').each(function (btn) {
            if ($(this).text() == 'Cancel') {
                $(this).removeAttr('disabled');
            }
        });

        var t = document.querySelectorAll(".workflow-settings-button").forEach((item) => {


            if (String(item.parentNode.tagName).toLowerCase() == "elsa-flyout-panel") {
                item.removeAttribute('disabled');
            }
            else if (String(item.parentNode.tagName).toLowerCase() == "div" && item.parentElement?.classList.contains('elsa-w-screen')) {

                item.removeAttribute('disabled');
            }
            else
                item.remove();
        });

    }, 0);

};


//Func to remove add workflow button
const rmCreateBtn = () => {
    $("a[href='/workflow-definitions/new']").parent().remove();
    $("elsa-context-menu").remove();
};

const elsaStudioRoot = document.querySelector('elsa-studio-root');
elsaStudioRoot.addEventListener('initializing', e => {
    const elsaStudio = e.detail;
    elsaStudio.pluginManager.registerPlugin(AuthorizationMiddlewarePlugin);
});

function AuthorizationMiddlewarePlugin(elsaStudio) {
    const eventBus = elsaStudio.eventBus;

    eventBus.on('http-client-created', e => {
        // Register Axios middleware.
        e.service.register({
            onRequest(request) {
                request.headers = { 'Authorization': 'Bearer ' + localStorage.accessToken, 'Accept': 'application/json', 'Content-Type': 'application/json;' }
                console.log(request);
                console.log(request.headers);
                return request; 
            }
        });
        e.service.register({
            onResponseError(error) {
                console.log("Response");
                console.log(error);

                alert("Somme error! Contact dev team");
            }
        });
    });
}