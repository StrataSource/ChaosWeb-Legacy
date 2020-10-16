
function build_sass() {
  echo "Compiling $1 sass"
  sass $1/boosted.scss css/boosted_$1.min.css --style compressed
  sass $1/boosted.scss css/boosted_$1.css
}


build_sass p2ce
build_sass chaos

cp -v css/boosted_chaos* ../ChaosInitiative.Web.HomePage/wwwroot/css/.
cp -v css/boosted_chaos* ../ChaosInitiative.Web.ControlPanel/wwwroot/css/.
cp -v css/boosted_p2ce* ../ChaosInitiative.Web.P2CE/wwwroot/css/.

