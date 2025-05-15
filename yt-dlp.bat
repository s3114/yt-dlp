echo off
chcp 65001
setlocal enabledelayedexpansion

:: 設定ファイルが存在するか確認
    goto :END
        set "setting_DIR=%SAVE_DIR%settings.txt"
        if exist "!setting_DIR!" (
            :: ファイルが存在する場合、1行目を読み込む
            for /f "usebackq delims=" %%a in ("!setting_DIR!") do (
                set "SAVE_DIR=%%a"

                )
            ) else (
            set /p "SAVE_DIR=[92m動画ダウンローダーの保存ディレクトリを入力してください ＞ [0m"
                if "%SAVE_DIR:~-1%"=="\" (
                    set SAVE_DIR=!SAVE_DIR!
                    echo 保存先：!SAVE_DIR!
                    echo !SAVE_DIR! > !setting_DIR!
                ) else (
                    set SAVE_DIR=!SAVE_DIR!\
                    echo 保存先：!SAVE_DIR!
                    echo !SAVE_DIR! > !setting_DIR!
                )

                echo.
                echo [91msettings.txtの新規作成を確認しました。ファイルを再起動してください。[0m
                pause
                exit

            )

        set "SAVE_DIR=%SAVE_DIR: =%"
        :END

:: 修正版、ディレクトリ読み取り
        set "SAVE_DIR=%~dp0"

        :: 使用ファイルの確認
        if exist "%SAVE_DIR%URLlist.txt" (
            echo URLlistを確認しました
        ) else (
            echo. > "%SAVE_DIR%URLlist.txt"
            echo URLlist.txtを作成しました
            )
        if exist "%SAVE_DIR%finished.txt" (
            echo finished.txtを確認しました
        ) else (
            echo finished.txtを作成しました
            echo. > "%SAVE_DIR%finished.txt"
            )

        set "finished=%SAVE_DIR%finished.txt"
        set "URL_list=%SAVE_DIR%URLlist.txt"



:: 使用ツールの取得、バージョン確認

    echo.
    echo [92m使用ツールのバージョンを確認しています。 [0m
    echo [92mffmpegを確認し、ダウンロード、アップデートを行います。 [0m

        if exist "%SAVE_DIR%ffmpeg.exe" (
            echo ダウンロードされたffmpeg.exeが検出されました。
            echo [93mffmpegを確認^(1/3^) [0m
        ) else (
            powershell -Command ^
                "$ffmpeg = winget list --name ffmpeg; " ^
                "if ($ffmpeg -like '*ffmpeg*') { " ^
                "    Write-Host 'ffmpeg is installed. Perform the update...'; " ^
                "    winget upgrade ffmpeg; " ^
                "    exit 0;"^
                "} else { " ^
                "    Write-Host 'ffmpeg is not installed. Start installation...'; " ^
                "    winget install ffmpeg; " ^
                "    exit 1;}"

            echo [93mffmpegを確認^(1/3^) [0m
            echo.

            if %ERRORLEVEL%==1 (
                echo [91mffmpegのインストールが確認されました。 [0m
                echo [91mファイルを再起動してください。 [0m
                pause
                exit
            )
        )


    echo.
    echo [92myt-dlpを確認し、ダウンロード、アップデートを行います。 [0m
            if exist "%SAVE_DIR%yt-dlp.exe" (
            echo ダウンロードされたyt-dlp.exeが検出されました。
            yt-dlp -U
            ) else (
                curl -L -o "%SAVE_DIR%yt-dlp.exe" "https://github.com/yt-dlp/yt-dlp/releases/download/2024.10.07/yt-dlp.exe"
                yt-dlp -U
            )
        echo [93m yt-dlpを確認（2/3）[0m

    echo.
    echo [92mAtomicParsleyを確認し、ダウンロードを行います。[0m
            if exist "%SAVE_DIR%AtomicParsley.exe" (
                echo ダウンロードされたAtomicParsley.exeが検出されました。
                rem エラー回避の記述
            ) else (
                curl -L -o "%SAVE_DIR%AtomicParsley.zip" "https://github.com/wez/atomicparsley/releases/download/20240608.083822.1ed9031/AtomicParsleyWindows.zip"
                if exist "%SAVE_DIR%AtomicParsley.zip" (
                    powershell -Command ^
                    "Expand-Archive -Path '%SAVE_DIR%AtomicParsley.zip' -DestinationPath '%SAVE_DIR%' -Force"
                    del "%SAVE_DIR%AtomicParsley.zip"
                )
            )
        echo [93mAtomicParsley.exeを確認(3/3) [0m


:: "downloadするURLを取得"
    echo.
    echo [96mダウンロードするURLを入力してください。 [0m
    echo [0m対応しているもの [0m
    echo [0m・youtubeの単体の動画 [0m
    echo [0m・youtubeのプレイリスト（cookie.txtがあればメン限も可） [0m
    echo [0m・youtubeのメン限の動画 [0m
    echo [0m・youtubeのチャンネル（メン限以外のすべての動画がダウンロードされます） [0m
    echo [0m・ABEMAの単体動画 [0m
    echo [0m・ABEMAの一期を一括 [0m
    echo [0m・ABEMAのバラを一括 [0m
    echo [0m（　先に、ダウンロードしたいABEMAの動画単体のURLを!URL_list!に記入しておいてください。） 
    echo [0m（　URLごとに改行してください。）[0m 
    echo [0m（　次に、ここにてABEMAtxtと入力してください。） [0m
    echo [0m [0m
    echo.
    set /p playlist_url=[96mURLを入力してください。＞ [0m
    :: "URLによって分岐"
        :: youtube
            :: youtubeプレイリストのURL
            echo !playlist_url! | findstr /c:"youtube.com/playlist?list=" >nul
            if %errorlevel%==0 (
                echo [31myoutubeのプレイリストが検出されました。 [0m
                yt-dlp "!playlist_url!" --cookies cookies.txt --flat-playlist --skip-download --get-url --quiet --no-warnings > "!URL_list!"
                echo downloadする再生リスト内の動画URLは"!URL_list!" に保存されました。 
                set platform=1
            )
            :: youtube動画のURL
            echo !playlist_url! | findstr /c:"youtube.com/watch?v=" /c:"youtu.be/" >nul
            if %errorlevel%==0 (
                echo [31myoutubeの動画が検出されました。 [0m
                echo !playlist_url! | findstr /c:"&index=" >nul
                if %errorlevel%==0 (
                    for /f "tokens=1 delims=&" %%b in ("%playlist_url%") do set "playlist_url=%%b"
                )
                echo !playlist_url! > "!URL_list!" 
                echo.
                echo 動画URLは"!URL_list!"に保存されました。 
                set platform=1
            )
            :: youtubeのチャンネル
            echo !playlist_url! | findstr /c:"youtube.com/@" /c:"youtube.com/channel" >nul
            if %errorlevel%==0 (
                echo [31myoutubeのチャンネルが検出されました。 [0m
                yt-dlp "!playlist_url!" --cookies cookies.txt --flat-playlist --skip-download --get-id --quiet --no-warnings > "!URL_list!"
                echo downloadするチャンネルの動画URLは"!URL_list!" に保存されました。 
                echo チャンネルによっては、非常に多くのストレージが必要になります。気を付けてください。
                set platform=1
            )

        :: ABEMA
            :: ABEMAシリーズのURL
            if not "!playlist_url!"=="!playlist_url:title=!" (
                yt-dlp "!playlist_url!" --cookies cookies.txt --flat-playlist --skip-download --get-url --quiet --no-warnings > "!URL_list!"
                echo downloadする再生リスト内の動画URLは"!URL_list!" に保存されました。＞ 
                set platform=2
            )
            :: ABEMA動画のURL
            if not "!playlist_url!"=="!playlist_url:episode=!" (
                echo !playlist_url! > "!URL_list!" 
                echo.
                echo 動画URLは"!URL_list!"に保存されました。＞ 
                set platform=2
            )
            :: ABEMA手動取得
            if not "!playlist_url!"=="!playlist_url:ABEMAtxt=!" (
                echo 動画URLは"!URL_list!"にすでに保存されているURLから取得します。
                set platform=2
            )

:: "downloadする画質を設定"
    if "%platform%"=="1" (
        :: 説明
            echo.
            echo.
            echo [96m1[0m、4320p・8K（7680 X 4320） 
            echo [96m2[0m、2160p・4K（3840 X 2160） 
            echo [96m3[0m、1440p    （2560 X 1440） 
            echo [96m4[0m、1080p・2K（1920 X 1080） 
            echo [96m5[0m、720p・HD （1280 X 720 ） 
            echo [96m6[0m、480p・SD （ 720 X 480 ） 
            echo [96m7[0m、360p     （ 640 X 360 ） 
            echo [96m8[0m、240p     （ 427 X 240 ） 
            echo [96m9[0m、144p     （ 256 X 144 ） 

        :: 割り当て
            echo 動画の画質が対応していない場合、ダウンロード可能な最高画質がダウンロードされます。 
            echo.
            set /p image_quality=[96mダウンロードする画質の番号を入力してください。（半角英数字）＞ [0m

            if !image_quality! equ 1 (
                set "download_quality=bv[height=4320][ext=mp4]+ba[ext=m4a]/bv[height=2160][ext=mp4]+ba[ext=m4a]/bv[height=1440][ext=mp4]+ba[ext=m4a]/bv[height=1080][ext=mp4]+ba[ext=m4a]/bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]"
                echo 4320pが選択されました。 
            )
            if !image_quality! equ 2 (
                set "download_quality=bv[height=2160][ext=mp4]+ba[ext=m4a]/bv[height=1440][ext=mp4]+ba[ext=m4a]/bv[height=1080][ext=mp4]+ba[ext=m4a]/bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]"
                echo 2160pが選択されました。 
            )
            if !image_quality! equ 3 (
                set "download_quality=bv[height=1440][ext=mp4]+ba[ext=m4a]/bv[height=1080][ext=mp4]+ba[ext=m4a]/bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]"
                echo 1440pが選択されました。 
            )
            if !image_quality! equ 4 (
                set "download_quality=bv[height=1080][ext=mp4]+ba[ext=m4a]/bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]"
                echo 1080pが選択されました。 
            )
            if !image_quality! equ 5 (
                set "download_quality=bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]"
                echo 720pが選択されました。 
            )
            if !image_quality! equ 6 (
                set "download_quality=bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]"
                echo 480pが選択されました。 
            )
            if !image_quality! equ 7 (
                set "download_quality=bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]"
                echo 360pが選択されました。 
            )
            if !image_quality! equ 8 (
                set "download_quality=bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]"
                echo 240pが選択されました。 
            )
            if !image_quality! equ 9 (
                set "download_quality=bv[height=144][ext=mp4]+ba[ext=m4a]"
                echo 144pが選択されました。 
            )
    )
    if "%platform%"=="2" (
        echo ABEMAが検出されました。ABEMAは画質の設定に対応しておりません。
    )


:: ダウンロード実行処理
    echo.
    echo [92mダウンロードが開始されます[0m
        echo "!URL_list!"
        if "%platform%"=="1" (

            echo.
            echo [96myoutubeの動画のコメントもダウンロードしますか？（試験的機能）[0m
            echo [96mこの機能では、ダウンロードする際により多くの時間がかかります[0m
            echo.
            echo [96m1[0m、ダウンロードしない。 
            echo [96m2[0m、コメントのみダウンロードする。 
            echo [96m3[0m、ライブ中のコメントのみダウンロードする。 
            echo [96m4[0m、すべてのコメントをダウンロードする。 
            set /p get_comments=[0m選択してください。 ＞ [0m

                if !get_comments! equ 1 (
                    for /f "delims=" %%u in (!URL_list!) do (
                        start /b yt-dlp "%%u" --cookies cookies.txt --write-thumbnail --embed-thumbnail --add-metadata --download-archive %finished% --ignore-errors -f "%download_quality%" --output "%SAVE-DIR%youtube\%%(upload_date)s-%%(title)s.%%(ext)s" -N 32 --fragment-retries 5 --retries infinite
                    )
                )
                if !get_comments! equ 2 (
                    for /f "delims=" %%u in (!URL_list!) do (
                        start /b yt-dlp "%%u" --cookies cookies.txt --get-comments --write-thumbnail --embed-thumbnail --add-metadata --download-archive %finished% --ignore-errors -f "%download_quality%" --output "%SAVE-DIR%youtube\%%(upload_date)s-%%(title)s.%%(ext)s" -N 32 --fragment-retries 5 --retries infinite
                    )
                )
                if !get_comments! equ 3 (
                    for /f "delims=" %%u in (!URL_list!) do (
                        start /b yt-dlp "%%u" --cookies cookies.txt --write-sub --write-thumbnail --embed-thumbnail --add-metadata --download-archive %finished% --ignore-errors -f "%download_quality%" --output "%SAVE-DIR%youtube\%%(upload_date)s-%%(title)s.%%(ext)s" -N 32 --fragment-retries 5 --retries infinite
                    )
                )
                if !get_comments! equ 4 (
                    for /f "delims=" %%u in (!URL_list!) do (
                        start /b yt-dlp "%%u" --cookies cookies.txt --get-comments --write-sub --write-thumbnail --embed-thumbnail --add-metadata --download-archive %finished% --ignore-errors -f "%download_quality%" --output "%SAVE-DIR%youtube\%%(upload_date)s-%%(title)s.%%(ext)s" -N 32 --fragment-retries 5 --retries infinite
                    )
                )
            )
            if "%platform%"=="2" (
                for /f "delims=" %%u in (!URL_list!) do (
                    start /b yt-dlp "%%u" --cookies cookies.txt --download-archive %finished% --ignore-errors --merge-output-format mp4 --output "!SAVE_DIR!ABEMA\%%(title)s.%%(ext)s" --no-check-certificate --geo-bypass-country JP --hls-prefer-native -N 32 --fragment-retries 5 --retries infinite
                )
            )

:: 全てのyt-dlpプロセスの終了を待機
    :wait_for_completion
    set "yt_dlp_running="
    for /f "tokens=1" %%p in ('tasklist /FI "IMAGENAME eq yt-dlp.exe" /NH') do (
        if /I "%%p"=="yt-dlp.exe" set "yt_dlp_running=1"
    )

    if defined yt_dlp_running (
        timeout /t 5 >nul
        goto wait_for_completion
    )

:: ダウンロード完了後の処置
    echo.
    echo [92m全ての動画のダウンロードが完了いたしました。 [0m
    echo.
    echo [92mファイルの整理を実行します。 [0m

    :: youtubeなら
    if "%platform%"=="1" (
        type null > sub.bat
        echo @echo off > sub.bat
        echo chcp 65001 >> sub.bat
        echo setlocal enabledelayedexpansion >> sub.bat
        echo md "%~dp0youtube\サムネイル" >> sub.bat
        echo md "%~dp0youtube\動画" >> sub.bat
        echo md "%~dp0youtube\コメント" >> sub.bat
        echo move "%~dp0youtube\*.webp" "%~dp0youtube\サムネイル\" >> sub.bat
        echo move "%~dp0youtube\*.mp4" "%~dp0youtube\動画\" >> sub.bat
        echo move "%~dp0youtube\*.json" "%~dp0youtube\コメント\" >> sub.bat


        call "%~dp0sub.bat" 
        del "%~dp0sub.bat"
        echo [96m全ての処理を完了しました。プログラムを終了します[0m
    )

pause
