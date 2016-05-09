var multiFilesDroppable = (function (window) {
    var _ele;

    function triggerCallback(e, callback) {
        if (!callback || typeof callback !== 'function') {
            return;
        }
        var files;
        if (e.dataTransfer) {
            files = e.dataTransfer.files;
        } else if (e.target) {
            files = e.target.files;
        }
        callback.call(null, files);
    }

    function initDroppable(ele, callback) {        
        var input = document.createElement('input');
        var gridTitle = document.createElement('div');

        input.setAttribute('type', 'file');
        input.setAttribute('multiple', true);
        input.setAttribute('id', 'dropFilesUpload');
        input.style.display = 'none';

        gridTitle.classList.add('dropHereTitle');
        gridTitle.innerText = 'DRAG FILES HERE';

        _ele = ele;

        input.addEventListener('change', function (e) {
            triggerCallback(e, callback);
        });

        ele.appendChild(input);
        ele.appendChild(gridTitle);

        ele.addEventListener('dragover', function (e) {
            e.preventDefault();
            e.stopPropagation();
            ele.classList.add('dragover');
        });

        ele.addEventListener('dragleave', function (e) {
            e.preventDefault();
            e.stopPropagation();
            ele.classList.remove('dragover');
        });

        ele.addEventListener('drop', function (e) {
            e.preventDefault();
            e.stopPropagation();
            ele.classList.remove('dragover');
            triggerCallback(e, callback);
        });

        // ele.addEventListener('click', function() {
        //     input.value = null;
        //     input.click();
        //     //console.log('click');
        // });
    }

    function destroy() {
        console.log(_ele);
        
    }

    return {
        initDroppable: initDroppable,
        destroy: destroy
    };
})();