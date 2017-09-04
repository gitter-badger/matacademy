var scene = (function () {
    "use strict";

    var scene = new THREE.Scene();
    var renderer = new THREE.WebGLRenderer({alpha:false});
    var camera, mesh, material;

    function initScene() {
        renderer.setSize(window.innerWidth, window.innerHeight);
        document.getElementById('container').appendChild(renderer.domElement);

        camera = new THREE.PerspectiveCamera(35, window.innerWidth / window.innerHeight, 1, 1000);
        camera.position.set(0, 0, 200);
        camera.rotation.z = 50;
        scene.add(camera);

        var loader = new THREE.TextureLoader();

        loader.load("./earth.jpg", function(texture) {
            material = new THREE.MeshLambertMaterial({map: texture});
            mesh = new THREE.Mesh(new THREE.SphereGeometry(40, 32, 32), material);
            scene.add(mesh);

            var directionalLight = new THREE.DirectionalLight( 0xffffff, 1 );
            directionalLight.position.set(50, 50, 40);
            var ambientLight = new THREE.AmbientLight( 0xffffff, 0.2 );
            scene.add(directionalLight);
            scene.add(ambientLight);

            render();
        });
    }

    function render() {
        requestAnimationFrame(render);
        renderer.render(scene, camera);
        mesh.rotation.y += 0.001;
    }
    return {
        initScene: initScene
    }
})();