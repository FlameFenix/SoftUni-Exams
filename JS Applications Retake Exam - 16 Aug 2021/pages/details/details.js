import { detailsTemplate } from "./detailsTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _gamesService = undefined;

function initialize(router, renderHandler, gamesService) {
    _router = router;
    _renderHandler = renderHandler;
    _gamesService = gamesService;
}

async function deleteHandler(id, e){
    try{
        await _gamesService.deleteItem(id);
        _router.redirect('/catalogue');
    } catch(err){
        alert(err);
    }
}

async function getView(context) {
    let id = context.params.id;
    let game = await _gamesService.get(id);
    let user = context.user;
    let isOwner = false;
    if(user !== undefined && user._id === game._ownerId){
        isOwner = true;
    }
    let model = {
        game,
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