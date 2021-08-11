import { detailsTemplate } from "./detailsTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _listingsService = undefined;

function initialize(router, renderHandler, listingsService) {
    _router = router;
    _renderHandler = renderHandler;
    _listingsService = listingsService;
}


async function deleteHandler(id, e){
    try{
        await _listingsService.deleteItem(id);
        _router.redirect('/listings');
    } catch(err){
        alert(err);
    }
}
async function getView(context) {
    let id = context.params.id;

    console.log(context.params);
    console.log(context.user);

    let listing = await _listingsService.get(id);

    let user = context.user;
    let isOwner = false;

    if(user !== undefined && user._id === listing._ownerId){
        isOwner = true;
    }

    let model = {
        listing,
        deleteHandler,
        isOwner
    };
    let templateResult = detailsTemplate(model);
    _renderHandler(templateResult);
}


export default {
    getView,
    initialize
}