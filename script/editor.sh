#
# trigger one of the Assets/Editor/MyEditorScript.cs build methods from the command line
#

function contains() {
    local n=$#
    local value=${!n}
    for ((i=1;i < $#;i++)) {
        if [ "${!i}" == "${value}" ]; then
            echo "y"
            return 0
        fi
    }
    echo "n"
    return 1
}

if [ -z "$UNITY_HOME" ]; then
  echo "Missing UNITY_HOME environment variable. Using default"
  UNITY_HOME=/Applications/Unity/Unity.app
fi

methods=(`sed  -n 's/.*static void Perform\(.*\)Build ().*/\1/p' Assets/Editor/MyEditorScript.cs | sort`)

if [ $# -ne 1 ]; then
  echo "ERROR: missing argument"
  echo "Usage: $0 method"
  echo "Methods available: ${methods[@]}"
  exit -1
fi

method=$1
if [ $(contains "${methods[@]}" "$method") == "n" ]; then
  echo "ERROR: unknown build target '$method'"
  echo "Usage: $0 method"
  echo "Methods available: ${methods[@]}"
  exit -1
fi

# ideally should be a number
if [ -z "$BUILD_NUMBER" ]; then
  idid=`id -u`
  id=`id -u -n $idid`
  now=`date +%Y%m%d%H%M%S`
  export BUILD_NUMBER="${now}"
fi

# as Jenkins's BUILD-ID: YYYY-MM-DD_hh-mm-ss
if [ -z "$BUILD_ID" ]; then
  bid=`date +%Y-%m-%d_%H-%M-%S`
  export BUILD_ID="${bid}"
fi

echo "BUILD_NUMBER: ${BUILD_NUMBER}"
echo "BUILD_ID: ${BUILD_ID}"
echo "USER: ${USER}"
echo "UNITY_HOME $UNITY_HOME"
PROJECT_PATH=`pwd`
echo "Building $WORKSPACE"
#rm -rf target
mkdir -p target
$UNITY_HOME/Contents/MacOS/Unity -projectpath $PROJECT_PATH -quit -batchmode  -executeMethod "MyEditorScript.Perform${method}Build"
buildres=$?
if [ $buildres -ne 0 ]; then
  cat ~/Library/Logs/Unity/Editor.log
  ls -la ~/Library/Logs/Unity/Editor.log
else
  echo "Success building $method"
  ls -la target
fi
exit $buildres
