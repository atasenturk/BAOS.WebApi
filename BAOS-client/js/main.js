const url = "https://localhost:44329/api";

const btnPredict = document.getElementById("btnPredict");
const loginErrorBtn = document.getElementById("loginErrorBtn");
var modal = document.getElementById("myModal");
const modalResult = document.getElementById("predResult");
const logoutBtn = document.getElementById("btn-logout");

var loginErrorModal = document.getElementById("loginErrorModal");

loadIndex();
showUser();

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

async function showUser() {
  const user = await getUser();
  const table = document.createElement("table");

  // Kullanıcı bilgileri
  const tbody = document.createElement("tbody");
  const dataRow = document.createElement("tr");
  const td1 = document.createElement("td");
  td1.textContent = user.id;
  const td2 = document.createElement("td");
  td2.textContent = user.userName;
  const td3 = document.createElement("td");
  td3.textContent = user.email;
  dataRow.appendChild(td1);
  dataRow.appendChild(td2);
  dataRow.appendChild(td3);
  tbody.appendChild(dataRow);
  table.appendChild(tbody);

  // Tabloyu HTML sayfasına ekleme
  const container = document.getElementById("card-body-div");
  container.appendChild(table);
}

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

    var partF =
      parseInt(
        Array.from(document.getElementsByName("Q16")).find((r) => r.checked)
          .value
      ) +
      parseInt(
        Array.from(document.getElementsByName("Q18")).find((r) => r.checked)
          .value
      );

    var partG = Array.from(document.getElementsByName("Q19")).filter(
      (r) => r.checked
    ).length;

    console.log(partA);
    console.log(partB);
    console.log(partC);
    console.log(partD);
    console.log(partF);
    console.log(partG);

    var values = [partA, partB, partC, partD, partF, partG, 5, 2, 2];
    return values;
  } catch (error) {
    alert("Her soruyu cevaplamanız gerekmektedir.");
  }
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

  const answersString = answers.join(" ");
  return answersString;
}

async function clickPredict(e) {
  e.preventDefault();

  const values = getAnswers();
  // const user = await getUser();
  const data = {
    // userId: user.id,
    answers: AnswersToString(),
    features: values,
  };

  console.log(data);

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
