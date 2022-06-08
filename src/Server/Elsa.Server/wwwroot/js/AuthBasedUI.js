$(document).ready(function () {

    if (localStorage.scopes == null)
        window.location = "/login";
 
    
    var wfDefinitionPerm;
    var wfInstancePerm;

    var tmp = JSON.parse(localStorage.scopes);
    tmp.forEach((obj) => {
        if (obj.rsName == 'workflow-definitions')
            wfDefinitionPerm = obj.scopes;

        if (obj.rsName == 'workflow-instances')
            wfInstancePerm = obj.scopes;
    });

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