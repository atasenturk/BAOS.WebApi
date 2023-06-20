const prevBtns = document.querySelectorAll(".btn-prev");
const nextBtns = document.querySelectorAll(".btn-next");
const progress = document.getElementById("progress");
const formSteps = document.querySelectorAll(".form-step");
const progressSteps = document.querySelectorAll(".progress-step");

let formStepsNum = 0;

nextBtns.forEach((btn) => {
  btn.addEventListener("click", () => {
    formStepsNum++;
    updateFormSteps();
    updateProgressBar();
  });
});

prevBtns.forEach((btn) => {
  btn.addEventListener("click", () => {
    formStepsNum--;
    updateFormSteps();
    updateProgressBar();
  });
});

function updateFormSteps() {
  formSteps.forEach((formStep) => {
    formStep.classList.contains("active") &&
      formStep.classList.remove("active");
  });
  formSteps[formStepsNum].classList.add("active");
}

function updateProgressBar() {
  progressSteps.forEach((step, index) => {
    if (index < formStepsNum + 1) {
      step.classList.add("active");
    } else {
      step.classList.remove("active");
    }
  });

  const progressActive = document.querySelectorAll(".progress-step.active");
  progress.style.width =
    ((progressActive.length - 1) / (progressSteps.length - 1)) * 100 + "%";
}
