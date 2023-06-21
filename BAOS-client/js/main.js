const url = "https://localhost:44329/api";
const urlLogin = "https://localhost:44329/api/user/login";
let urlUpdate = "https://localhost:44329/api/user/";
const btnPredict = document.getElementById("btnPredict");
const loginErrorBtn = document.getElementById("loginErrorBtn");
var modal = document.getElementById("myModal");
const modalResult = document.getElementById("predResult");
const logoutBtn = document.getElementById("btn-logout");

var loginErrorModal = document.getElementById("loginErrorModal");

loadIndex();

function loadIndex(e) {
  let lblGreeding = document.getElementById("greeding");
  getUser().then((user) => {
    lblGreeding.textContent = "Hoşgeldiniz " + user.userName;
  });
}

async function getUser(e) {
  try {
    if (localStorage.getItem("currentLoggedIn") === null) {
      loginErrorModal.style.display = "block";
    } else {
      let email = localStorage.getItem("currentLoggedIn");
      var response = await fetch(url + "/User/" + email);
    }
  } catch (error) {
    console.log(error);
    loginErrorModal.style.display = "block";
  }

  const data = await response.json();
  return data;
}

function findSelectedOptions(form) {
  var selectedOptions = [];
  var checkboxes = form.querySelectorAll('input[name="Q19"]:checked');
  checkboxes.forEach(function (checkbox) {
    selectedOptions.push(checkbox.value);
  });
  return selectedOptions;
}

document
  .getElementById("btnPredict")
  .addEventListener("click", function (event) {
    event.preventDefault();
    var form = document.querySelector(".form-step.active"); // Aktif formu seçmek için uygun bir seçici kullanın
    selectedOptionsQ19 = findSelectedOptions(form);
  });

function getAnswers() {
  try {
    var partA =
      parseInt(
        Array.from(document.getElementsByName("Q1")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q2")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q3")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q4")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q5")).find((r) => r.checked)
          .value
      );

    var partB =
      parseInt(
        Array.from(document.getElementsByName("Q6")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q7")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q8")).find((r) => r.checked)
          .value
      );

    var partC =
      parseInt(
        Array.from(document.getElementsByName("Q9")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q10")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q11")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q12")).find((r) => r.checked)
          .value
      );

    var partD =
      parseInt(
        Array.from(document.getElementsByName("Q13")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q14")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q15")).find((r) => r.checked)
          .value
      );

    var partE = parseInt(
      Array.from(document.getElementsByName("Q16")).find((r) => r.checked).value
    );

    var partF =
      parseInt(
        Array.from(document.getElementsByName("Q17")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q18")).find((r) => r.checked)
          .value
      );

    var partG = Array.from(document.getElementsByName("Q19")).filter(
      (r) => r.checked
    ).length;

    var threeParameter = createLastParameters();
    var values = [
      partA,
      partB,
      partC,
      partD,
      partE,
      partF,
      threeParameter[0],
      threeParameter[1],
      threeParameter[2],
    ];

    return values;
  } catch (error) {
    alert("Her soruyu cevaplamanız gerekmektedir.");
  }
}

function createLastParameters() {
  // 0, 1, 4
  let twoDimensionalArray = [
    [4, 7, 5],
    [2, 1, 1],
    [1, 5, 7],
    [8, 8, 8],
    [10, 9, 9],
    [10, 9, 9],
    [4, 2, 3],
    [7, 10, 3],
    [3, 1, 1],
    [7, 10, 4],
    [3, 1, 1],
  ];

  let minValues = [];

  for (let j = 0; j < 3; j++) {
    let min = twoDimensionalArray[selectedOptionsQ19[0]][j];
    for (let i = 1; i < selectedOptionsQ19.length; i++) {
      const element = twoDimensionalArray[selectedOptionsQ19[i]][j];
      if (element < min) min = twoDimensionalArray[selectedOptionsQ19[i]][j];
    }
    minValues.push(min);
  }

  return minValues;
}

function AnswersToString() {
  const answers = [];
  for (let i = 1; i <= 18; i++) {
    const selectedOption = Array.from(document.getElementsByName(`Q${i}`)).find(
      (r) => r.checked
    );
    if (selectedOption) {
      answers.push(parseInt(selectedOption.value));
    }
  }

  var answersString = answers.join(" ");
  answersString += " " + selectedOptionsQ19.join(" ");
  return answersString;
}

async function clickPredict(e) {
  e.preventDefault();

  const values = getAnswers();
  const user = await getUser();
  const data = {
    userId: user.id,
    answers: AnswersToString(),
    features: values,
  };

  await fetch(url + "/BAOSModel", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })
    .then((response) => response.text())
    .then((result) => {
      modal.style.display = "block";
      modalResult.innerHTML = result;
    });
}

function clickLogin() {
  location.href = "login.html";
}

function clickLogout() {
  localStorage.removeItem("currentLoggedIn");
}

window.onclick = function (event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
};

btnPredict.addEventListener("click", clickPredict);
loginErrorBtn.addEventListener("click", clickLogin);
logoutBtn.addEventListener("click", clickLogout);
