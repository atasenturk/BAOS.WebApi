const btnLogin = document.getElementById("btnLogin");
const btnRegister = document.getElementById("btnRegister");
const urlLogin = "https://localhost:44329/api/user/login"
const urlRegister = "https://localhost:44329/api/user/register"
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
async function disableInputs() {
  document.getElementById("username").disabled = true;
  document.getElementById("btnRegister").disabled = true;
  document.getElementById("emailRegister").disabled = true;
  document.getElementById("pswdRegister").disabled = true;
  document.getElementById("email").disabled = true;
  document.getElementById("pswd").disabled = true;
}

async function enableInputs() {
  document.getElementById("username").disabled = false;
  document.getElementById("btnRegister").disabled = false;
  document.getElementById("emailRegister").disabled = false;
  document.getElementById("pswdRegister").disabled = false;
  
  document.getElementById("email").disabled = false;
  document.getElementById("pswd").disabled = false;
}

async function clickRegister(e) {
  e.preventDefault();
  var username = document.getElementById("username").value;
  var email = document.getElementById("emailRegister").value;
  var pswd = document.getElementById("pswdRegister").value;

  disableInputs();

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
        alert("Kullanıcı e-posta veya kullanıcı adının sistemde kaydı var!");
      }
  });

  enableInputs();
}

window.onclick = function(event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
}

btnLogin.addEventListener("click", clickLogin);
btnRegister.addEventListener("click", clickRegister);

