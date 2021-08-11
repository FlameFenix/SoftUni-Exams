import { detailsTemplate } from "./detailsTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _booksService = undefined;

function initialize(router, renderHandler, booksService) {
    _router = router;
    _renderHandler = renderHandler;
    _booksService = booksService;
}

async function deleteHandler(id, e){
    try{
        await _booksService.deleteItem(id);
        _router.redirect('/dashboard');
    } catch(err){
        alert(err);
    }
}

async function getView(context) {
    let id = context.params.id;
    let book = await _booksService.get(id);
    let user = context.user;
    let isOwner = false;
    if(user !== undefined && user._id === book._ownerId){
        isOwner = true;
    }
    let model = {
        book,
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