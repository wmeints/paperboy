"""
Development settings for Paperboy project.
"""

import os
from dotenv import load_dotenv
from paperboy.settings.base import *  # noqa

# Load the .env file
load_dotenv()

# SECURITY WARNING: keep the secret key used in production secret!
SECRET_KEY = "django-insecure-@sjy%s*b9c$hd$=05ch_t1+hz1ik(em555#u)r05u_7@7tekau"

# SECURITY WARNING: don't run with debug turned on in production!
DEBUG = True

ALLOWED_HOSTS = []

# Database
# https://docs.djangoproject.com/en/5.1/ref/settings/#databases

DATABASES = {
    "default": {
        "ENGINE": "django.db.backends.postgresql",
        "NAME": os.getenv("APP_DB_NAME", ""),
        "HOST": os.getenv("APP_DB_HOST", ""),
        "USER": os.getenv("APP_DB_USER", ""),
        "PASSWORD": os.getenv("APP_DB_PASSWORD", ""),
    }
}
