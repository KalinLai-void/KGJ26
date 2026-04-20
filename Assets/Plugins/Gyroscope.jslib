mergeInto(LibraryManager.library, {
    RequestGyroPermission: function () {
        if (typeof DeviceOrientationEvent !== 'undefined' && typeof DeviceOrientationEvent.requestPermission === 'function') { // check the browser if need to request permission
            DeviceOrientationEvent.requestPermission()
                .then(response => {
                    if (response == 'granted') {
                        console.log("Permission access!");
                    } else {
                        console.warn("Permission is denied!");
                    }
                })
                .catch(console.error);
        } else {
            // Non-iOS deveice don't need.
        }
    }
});