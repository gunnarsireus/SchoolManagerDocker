// Write your JavaScript code.
$(document).ready(function () {
    timerJob();
    console.log('documentReady');
});

$(".uppercase").keyup(function () {
    var text = $(this).val();
    $(this).val(text.toUpperCase());
});

function clearErrors() {
    $(".validation-summary-errors").empty();
};

function timerJob() {
    const tenSeconds = 10000;
    const oneSecond = 1000;
    $.ajax({
        url: "http://localhost:54411/api/student",
        type: "GET",
        dataType: "json",
        success: function (students) {
            if (students.length === 0) {
                setTimeout(timerJob, oneSecond);
                console.log("No students found!");
                return;
            }
            const selectedItem = Math.floor(Math.random() * students.length);
            let selectedStudent = students[selectedItem];
            if (selectedStudent.disabled === true) {
                console.log(selectedStudent.lastName + " is blocked for uppdating of Present/Absent!");
                return;
            }
            selectedStudent.present = !selectedStudent.present;
            $.ajax({
                url: 'http://localhost:54411/api/student/' + selectedStudent.id,
                contentType: "application/json",
                type: "PUT",
                data: JSON.stringify(selectedStudent),
                dataType: "json"
            });

            const selector = `#${selectedStudent.id} td:eq(2)`;
            const selector2 = `#${selectedStudent.id + "_2"} td:eq(3)`;
            const selector3 = `#${selectedStudent.id + "_3"}`;
            if (selectedStudent.present === true) {
                $(selector).text("Present");
                $(selector).removeClass("alert-danger");
                $(selector2).text("Present");
                $(selector2).removeClass("alert-danger");
                $(selector3).text("Present");
                $(selector3).removeClass("alert-danger");
                console.log(selectedStudent.firstName + " " + selectedStudent.lastName + " is Present!");
            }
            else {
                $(selector).text("Absent");
                $(selector).addClass("alert-danger");
                $(selector2).text("Absent");
                $(selector2).addClass("alert-danger");
                $(selector3).text("Absent");
                $(selector3).addClass("alert-danger");
                console.log(selectedStudent.firstName + " " + selectedStudent.lastName + " is Absent!");
            }
            if (document.getElementById("All") !== null) {
                doFiltering();
            }
        }
    });
    setTimeout(timerJob, tenSeconds);
}

function doFiltering() {
    let selection = 0;
    let radiobtn = document.getElementById("All");
    if (radiobtn.checked === false) {
        radiobtn = document.getElementById("Present");
        if (radiobtn.checked === true) {
            selection = 1;
        }
        else {
            selection = 2;
        }
    }

    var table = $('#students > tbody');
    $('tr', table).each(function () {
        $(this).removeClass("hidden");
        let td = $('td:eq(2)', $(this)).html();
        if (td !== undefined) {
            td = td.trim();
        }
        if (td === "Absent" && selection === 1) {
            $(this).addClass("hidden");  //Show only Present
        }
        if (td === "Present" && selection === 2) {
            $(this).addClass("hidden"); //Show only Absent
        }
    });
};

function showModals() {
    window.open("./html/Courses.html", "_blank", "toolbar=yes,scrollbars=yes,resizable=yes,top=50,left=50,width=500,height=400");
}
