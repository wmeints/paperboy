/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["**/*.razor"],
  theme: {
    extend: {},
  },
  plugins: [
      require("daisyui")
  ],
  daisyui: {
    logs: false,
    themes: false
  }
}

