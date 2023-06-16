const btnLogin = document.getElementById("btnLogin");
const btnRegister = document.getElementById("btnRegister");
const urlLogin = "https://localhost:7089/api/user/login"
const urlRegister = "https://localhost:7089/api/user/register"
var modal = document.getElementById("myModal");
const modalResult = document.getElementById("predResult");

async function clickLogin(e) {
  e.preventDefault();
  var email = document.getElementById("email").value;
  var pswd = document.getElementById("pswd").value;

  let data = {
    email: email,
    password: pswd
  }

  await fetch(urlLogin, {
  method: "POST",
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify(data)      
  })
  .then(response => {
      if(response.status === 200){
        localStorage.setItem("currentLoggedIn",email);
        location.href = "index.html";
      }

      else if(response.status === 400){
          modal.style.display = "block";
      } else {
        throw new Error('Network response was not ok.');
      }
  }).catch(error => {
    console.error(error);
  });
}


async function clickRegister(e) {
  e.preventDefault();
  var username = document.getElementById("username").value;
  var email = document.getElementById("emailRegister").value;
  var pswd = document.getElementById("pswdRegister").value;

  let data = {
    username: username,
    email: email,
    password: pswd
  }

  await fetch(urlRegister, {
  method: "POST",
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify(data)      
  })
  .then(response => {
      if(response.status === 200){
        alert("Kullanıcı başarılı bir şekilde oluşturuldu!");
      }
      else if(response.status === 400) {
        alert("Kullanıcı e-postasının sistemde kaydı var!");
      }
  });
}

window.onclick = function(event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
}

btnLogin.addEventListener("click", clickLogin);
btnRegister.addEventListener("click", clickRegister);

