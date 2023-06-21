function toggle(btnID, eIDs) {
  // Feed the list of ids as a selector
  var theRows = document.querySelectorAll(eIDs);
  // Get the button that triggered this
  var theButton = document.getElementById(btnID);
  // If the button is not expanded...
  if (theButton.getAttribute("aria-expanded") == "false") {
    // Loop through the rows and show them
    for (var i = 0; i < theRows.length; i++) {
      theRows[i].classList.add("shown");
      theRows[i].classList.remove("hidden");
    }
    // Now set the button to expanded
    theButton.setAttribute("aria-expanded", "true");
    // Otherwise button is not expanded...
  } else {
    // Loop through the rows and hide them
    for (var i = 0; i < theRows.length; i++) {
      theRows[i].classList.add("hidden");
      theRows[i].classList.remove("shown");
    }
    // Now set the button to collapsed
    theButton.setAttribute("aria-expanded", "false");
  }
}

const tableBody = document.getElementById("table-body");

// profile sayfasındaki kullanıcı bilgileri
const inputUsername = document.getElementById("input-username");
const inputEmail = document.getElementById("input-email");
const inputPassword = document.getElementById("input-password");
const inputNewPassword = document.getElementById("input-new-password");

//profil sayfasındaki güncelle butonu
const btnUpdate = document.getElementById("update-user-info");
btnUpdate.addEventListener("click", updateUserInfo);

loadProfile();
getRequests();

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

async function loadProfile() {
  let user = await getUser();
  inputUsername.value = user.userName;
  inputUsername.disabled = true;
  inputEmail.value = user.email;
  inputEmail.disabled = true;
}

async function getRequests() {
  const user = await getUser();
  let url = `https://localhost:44329/api/User/requests/${user.id}`;

  fetch(url)
    .then((response) => response.json())
    .then((json) => {
      showRequests(json);
    });
}

const protocols = {
  1: "LAN",
  2: "WAN",
  3: "LPWAN",
};

// check if the password is true and call sendUpdateRequest
async function updateUserInfo(e) {
  e.preventDefault();

  let email = localStorage.getItem("currentLoggedIn");
  let password = inputPassword.value;

  let data = {
    email: email,
    password: password,
  };

  await fetch(urlLogin, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })
    .then((response) => {
      if (response.status === 200) {
        sendUpdateRequest();
      } else if (response.status === 400) {
        console.log("Şifre güncellenemiyor şifre yanlış");
      } else {
        throw new Error("Network response was not ok.");
      }
    })
    .catch((error) => {
      console.error(error);
    });
}

async function sendUpdateRequest() {
  const user = await getUser();
  let url = urlUpdate + user.id;

  let payload = {
    userName: user.userName,
    email: localStorage.getItem("currentLoggedIn"),
    password: inputNewPassword.value,
  };

  let options = {
    method: "PUT",
    body: JSON.stringify(payload),
  };

  fetch(url, options).then((response) => console.log(response));
}

async function showRequests(requests) {
  console.log(requests);

  requests.forEach((request, i) => {
    let requestId = request.requestId;
    let questions = [];
    let questionIds = [];
    let answerIndex = 0;
    let answers = answerStringToData(request.answers);

    for (let i = 1; i <= 19; i++) {
      let questionId = `r${requestId}q${i}`;
      questions.push(`<tr id="${questionId}" class="hidden">
        <td>${Object.keys(answers)[i - 1]}</td>
        <td>${Object.values(answers)[i - 1]}</td>
        <td></td>
        <td></td>
      </tr>`);
      questionIds.push(`#${questionId}`);
      answerIndex += 2;
      console.log();
    }

    tableBody.innerHTML += `<tr>
      <td>
        <button type="button" id="r${requestId}" aria-expanded="false" onclick="toggle(this.id,'${questionIds.join(
      ","
    )}');"   >
          <svg xmlns="\http://www.w3.org/2000/svg&quot;" viewBox="0 0 80 80" focusable="false"><path d="M70.3 13.8L40 66.3 9.7 13.8z"></path></svg>
        </button>
      </td>
      <td>${i + 1}</td>
      <td>${formatDate(request.requestTime)}</td>
      <td>${protocols[request.protocol]}</td>
    </tr>`.concat(questions.join(" "));
  });
}

function formatDate(dateString) {
  const date = new Date(dateString);

  const formattedDate = new Intl.DateTimeFormat("tr-TR", {
    year: "numeric",
    month: "long",
    day: "numeric",
    hour: "numeric",
    minute: "numeric",
    second: "numeric",
  }).format(date);

  return formattedDate;
}

function answerStringToData(str) {
  // var str = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 3";

  var arr = answerStringToArray(str);
  data = {
    "Binanın bulunduğu bölgenin dağlık/tepelik arazi yoğunluğunu nasıl tanımlarsınız?":
      "",
    "Bina bir ovada mı konumlanıyor?": "",
    "Bina ormana yakın mı?": "",
    "Binanın suya(deniz, nehir vb.) yakınlığı nedir?": "",
    "Binanın bulunduğu bölgede yağış miktarını nasıl tanımlarsınız?": "",
    "Binanın bulunduğu bölge şehir merkezinde veya kırsalda mı bulunuyor?": "",
    "Binanın bulunduğu bölgedeki binaların genel yüksekliği nasıl?": "",
    "Binanın bulunduğu bölgedeki binaların sıklığı nasıl?": "",
    "Binanın yapı malzemesi nedir?": "",
    "Binanın cam oranı nasıldır?": "",
    "Evde kaç oda bulunuyor?": "",
    "Eviniz kaç metrekare?": "",
    "Evde kaç kişi yaşıyor?": "",
    "Evde 50 yaş ve üzerinde kaç kişi var?": "",
    "Evde evcil hayvan sayısı kaçtır?": "",
    "Dairede yaşayan insanların yaş ortalaması nedir?": "",
    "Evde kamera sistemi var mı?": "",
    "Evde kaç adet kamera var?": "",
    "Akıllı evinizde aşağıdakilerden hangilerine ihtiyacınız var?": "",
  };

  switch (arr[0]) {
    case 1:
      data[
        "Binanın bulunduğu bölgenin dağlık/tepelik arazi yoğunluğunu nasıl tanımlarsınız?"
      ] = "Dağlık";
      break;
    case 2:
      data[
        "Binanın bulunduğu bölgenin dağlık/tepelik arazi yoğunluğunu nasıl tanımlarsınız?"
      ] = "Kısmen dağlık";
      break;
    case 3:
      data[
        "Binanın bulunduğu bölgenin dağlık/tepelik arazi yoğunluğunu nasıl tanımlarsınız?"
      ] = "Dağ yok";
      break;
  }

  switch (arr[1]) {
    case 1:
      data["Bina bir ovada mı konumlanıyor?"] = "Evet";
      break;
    case 0:
      data["Bina bir ovada mı konumlanıyor?"] = "Hayır";
      break;
  }

  switch (arr[2]) {
    case 0:
      data["Bina ormana yakın mı?"] = "Ormanın içinde";
      break;
    case 1:
      data["Bina ormana yakın mı?"] = "Ormana yakın";
      break;
    case 2:
      data["Bina ormana yakın mı?"] = "Değil";
      break;
  }

  switch (arr[3]) {
    case 0:
      data["Binanın suya(deniz, nehir vb.) yakınlığı nedir?"] = "1 km'den az";
      break;
    case 1:
      data["Binanın suya(deniz, nehir vb.) yakınlığı nedir?"] = "1-5 km arası";
      break;
    case 2:
      data["Binanın suya(deniz, nehir vb.) yakınlığı nedir?"] = "Yakın değil";
      break;
  }

  switch (arr[4]) {
    case 0:
      data["Binanın bulunduğu bölgede yağış miktarını nasıl tanımlarsınız?"] =
        "Genelde yağışlı";
      break;
    case 1:
      data["Binanın bulunduğu bölgede yağış miktarını nasıl tanımlarsınız?"] =
        "Nadiren yağışlı";
      break;
    case 2:
      data["Binanın bulunduğu bölgede yağış miktarını nasıl tanımlarsınız?"] =
        "Yağış yok";
  }

  switch (arr[5]) {
    case 0:
      data[
        "Binanın bulunduğu bölge şehir merkezinde veya kırsalda mı bulunuyor?"
      ] = "Şehir merkezinde";
      break;
    case 2:
      data[
        "Binanın bulunduğu bölge şehir merkezinde veya kırsalda mı bulunuyor?"
      ] = "Kırsalda";
      break;
  }

  switch (arr[6]) {
    case 4:
      data["Binanın bulunduğu bölgedeki binaların genel yüksekliği nasıl?"] =
        "Genelde tek katlı binalar";
      break;
    case 3:
      data["Binanın bulunduğu bölgedeki binaların genel yüksekliği nasıl?"] =
        "Genelde 4-5 katlı binalar";
      break;
    case 2:
      data["Binanın bulunduğu bölgedeki binaların genel yüksekliği nasıl?"] =
        "Genelde 10 kat ve üzeri binalar";
      break;
    case 1:
      data["Binanın bulunduğu bölgedeki binaların genel yüksekliği nasıl?"] =
        "Gökdelen yoğunlukta";
      break;
  }

  switch (arr[7]) {
    case 0:
      data["Binanın bulunduğu bölgedeki binaların sıklığı nasıl?"] =
        "Binalar birbirine çok yakın";
      break;
    case 2:
      data["Binanın bulunduğu bölgedeki binaların sıklığı nasıl?"] =
        "Binalar bölgede seyrek şekilde yerleşmiş";
      break;
    case 4:
      data["Binanın bulunduğu bölgedeki binaların sıklığı nasıl?"] =
        "Binalar arası mesafeler var";
      break;
  }

  switch (arr[8]) {
    case 4:
      data["Binanın yapı malzemesi nedir?"] = "Çelik";
      break;
    case 3:
      data["Binanın yapı malzemesi nedir?"] = "Beton";
      break;
    case 2:
      data["Binanın yapı malzemesi nedir?"] = "Kerpiç";
      break;
    case 1:
      data["Binanın yapı malzemesi nedir?"] = "Ahşap";
      break;
    case 0:
      data["Binanın yapı malzemesi nedir?"] = "Prefabrik";
      break;
  }

  switch (arr[9]) {
    case 1:
      data["Binanın cam oranı nasıldır?"] = "Az sayıda cam var";
      break;
    case 2:
      data["Binanın cam oranı nasıldır?"] = "Orta sayıda cam var";
      break;
    case 0:
      data["Binanın cam oranı nasıldır?"] = "Çok sayıda cam var";
      break;
  }

  switch (arr[10]) {
    case 1:
      data["Evde kaç oda bulunuyor?"] = "1-2 oda";
      break;
    case 2:
      data["Evde kaç oda bulunuyor?"] = "3-4 oda";
      break;
    case 0:
      data["Evde kaç oda bulunuyor?"] = "5 ve üzeri oda";
      break;
  }

  switch (arr[11]) {
    case 4:
      data["Eviniz kaç metrekare?"] = "0-55 m2";
      break;
    case 3:
      data["Eviniz kaç metrekare?"] = "56-120 m2";
      break;
    case 2:
      data["Eviniz kaç metrekare?"] = "121-200 m2";
      break;
    case 1:
      data["Eviniz kaç metrekare?"] = "201 ve üzeri m2";
      break;
  }

  switch (arr[12]) {
    case 4:
      data["Evde kaç kişi yaşıyor?"] = "1-2 kişi";
      break;
    case 2:
      data["Evde kaç kişi yaşıyor?"] = "3-4 kişi";
      break;
    case 0:
      data["Evde kaç kişi yaşıyor?"] = "5 ve üzeri kişi";
      break;
  }

  switch (arr[13]) {
    case 4:
      data["Evde 50 yaş ve üzerinde kaç kişi var?"] = "0 kişi";
      break;
    case 2:
      data["Evde 50 yaş ve üzerinde kaç kişi var?"] = "1-2 kişi";
      break;
    case 1:
      data["Evde 50 yaş ve üzerinde kaç kişi var?"] = "3 ve üzeri kişi";
      break;
  }

  switch (arr[14]) {
    case 2:
      data["Evde evcil hayvan sayısı kaçtır?"] = "0 kişi";
      break;
    case 1:
      data["Evde evcil hayvan sayısı kaçtır?"] = "1-2";
      break;
    case 0:
      data["Evde evcil hayvan sayısı kaçtır?"] = "3 ve üzeri";
      break;
  }

  switch (arr[15]) {
    case 10:
      data["Dairede yaşayan insanların yaş ortalaması nedir?"] = "25'ten küçük";
      break;
    case 5:
      data["Dairede yaşayan insanların yaş ortalaması nedir?"] =
        "25-50 aralığında";
      break;
    case 0:
      data["Dairede yaşayan insanların yaş ortalaması nedir?"] = "50'den fazla";
      break;
  }

  switch (arr[16]) {
    case 1:
      data["Evde kamera sistemi var mı?"] = "Var";
      break;
    case 4:
      data["Evde kamera sistemi var mı?"] = "Yok";
      break;
  }

  switch (arr[17]) {
    case 6:
      data["Evde kaç adet kamera var?"] = "0";
      break;
    case 4:
      data["Evde kaç adet kamera var?"] = "1-3";
      break;
    case 2:
      data["Evde kaç adet kamera var?"] = "4-6";
      break;
    case 0:
      data["Evde kaç adet kamera var?"] = "7 ve üzeri";
      break;
  }

  const labels = [
    "Ev Aydınlatma Kontrol Sistemleri",
    "Güvenlik Sistemleri",
    "Hareketli Aydınlatma Kontrolü Sensörleri",
    "Termostatlar",
    "Akıllı Fişler",
    "Akıllı Anahtarlı Uzatma Kabloları",
    "Fırın, ocak uzaktan kontrol",
    "Bahçe uzaktan kontrol sulama sistemleri",
    "Çamaşır makinesi uzaktan kontrol",
    "Gaz kaçağı, yangın vs. anlarında telefona anlık bildirim gelmesi",
    "Buzdolabı içeriği görebilme",
    "Kapı zorlamasına karşı, ev içinde siren çalması, ev dışındakilere ise telefon bildirimi gönderilmesi",
  ];

  let checkBoxArr = checkboxAnswersToArray(str);
  let checkBoxStr = [];
  for (let i = 0; i < checkBoxArr.length; i++) {
    checkBoxStr.push(labels[checkBoxArr[i]]);
    data["Akıllı evinizde aşağıdakilerden hangilerine ihtiyacınız var?"] =
      checkBoxStr.join(", ");
  }

  return data;
}

function answerStringToArray(str) {
  let numbersArray = [];
  let numbers = str.split(" ");

  for (let i = 0; i < 18; i++) {
    numbersArray.push(parseInt(numbers[i]));
  }

  return numbersArray;
}

function checkboxAnswersToArray(str) {
  let numbersArray = [];
  let numbers = str.split(" ");

  for (let i = 18; i < numbers.length; i++) {
    numbersArray.push(parseInt(numbers[i]));
  }
  return numbersArray;
}
