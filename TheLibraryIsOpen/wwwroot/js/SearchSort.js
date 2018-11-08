let sortBy = $("#sortBy");
let orderBy = $("#orderBy");
let resultView = $("#results");
let orderables = $(".orderable");

sortBy.on('change', () => {
    sort(sortBy.val(), orderBy.val());
});

orderBy.on('change', () => {
    sort(sortBy.val(), orderBy.val());
});

function sort(option, order) {
    var attrib;
    if (option === "name")
        attrib = 'data-name';
    else if (option === "type")
        attrib = 'data-type';
    else if (option === "date")
        attrib = 'data-date';

    orderables = orderables.sort((a, b) => {
        let contentA = $(a).attr(attrib);
        let contentB = $(b).attr(attrib);

        if (order === "asc") {
            return contentA.localeCompare(contentB);
        } else {
            return contentB.localeCompare(contentA);
        }
    });

    resultView.html(orderables);
}