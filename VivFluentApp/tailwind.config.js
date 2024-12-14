/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Components/**/*.{razor,html}",
    "./Components/*.razor",
  ],
  theme: {
    extend: {
      fontFamily: {
        poppins: ['Poppins', 'sans-serif'],
        ibm: ['IBM Plex Sans', 'sans-serif'],
        cute: ['Cute Font', 'sans-serif'],
        noto: ['Noto Sans KR', 'sans-serif'],
        firacode: ['Fira Code', 'monospace']
      },
      backgroundImage: {
        'robot': "url('../images/robot-man.webp')"
      },
    },
  },
  plugins: [],
}

